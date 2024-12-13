using EmpManagementApi.Context;
using EmpManagementApi.Helper;
using EmpManagementApi.IRepository;
using EmpManagementApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpManagementApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Config.ConStr = Configuration["AppSettings:ConnectionStr"];
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IEmpRepository, EmpRepository>();
            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // Angular's default port
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MANTRA@InnovationThatCounts"))
                 };
             });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("SuperRights", policy => policy.RequireRole("Admin", "Manager", "Employee"));
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            //    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
            //    options.AddPolicy("Employee", policy => policy.RequireRole("Employee"));
            //});

            //  // Jwt Token
            ////  var key = Encoding.ASCII.GetBytes(Configuration["JWT:Key"]);
            //  var key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
            //  services.AddAuthentication(x =>
            //  {
            //      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     // x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //  }).AddJwtBearer(x =>
            //  {
            //      x.RequireHttpsMetadata = false;
            //      x.SaveToken = true;
            //      x.TokenValidationParameters = new TokenValidationParameters
            //      {
            //          ValidateIssuerSigningKey = true,

            //          IssuerSigningKey = new SymmetricSecurityKey(key),
            //          ValidateIssuer = true,
            //          ValidateAudience = true,
            //          ValidateLifetime = true,
            //          ValidIssuer = Configuration["JWT:Issuer"],
            //          ValidAudience = Configuration["JWT:Audience"],
            //          ClockSkew = TimeSpan.Zero
            //      };
            //  });

            services.AddMemoryCache();
            services.AddLazyCache();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Ensure CORS is applied before Authentication and Authorization
            app.UseCors("AllowSpecificOrigins");

            // Use JWT Middleware before Authentication and Authorization
            app.UseMiddleware<JWTMiddleware>();

            // Authentication middleware
            app.UseAuthentication();

            // Authorization middleware
            app.UseAuthorization();

            // Enable Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmpManagement API V1");
                c.RoutePrefix = "swagger";
            });

            // Map controllers and default route
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // For API Controllers
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }



    }
}
