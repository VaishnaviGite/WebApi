using EmpManagementApi.IRepository;
using EmpManagementApi.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpManagementApi.Helper
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;


        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        
        }

        public async Task Invoke(HttpContext Context, IAuthRepository authRepository)
        {
            try
            {
                string authHeader = Context.Request.Headers["Authorization"];
                var token = Context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                //Config.ConStrFromHeader = CS;
                //Config.TokenFromHeader = authHeader;

                if (token != null)
                {
                    attachUserToContext(Context, authRepository, token);
                }
                await _next(Context);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

           
        }


        private void attachUserToContext(HttpContext context, IAuthRepository authRepository, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("MANTRA@InnovationThatCounts");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.FromMinutes(20)
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);
                var userName = (jwtToken.Claims.First(x => x.Type == "sub").Value);

                // attach user to context on successful jwt validation
                ResGetUser obj = new ResGetUser();
                obj = authRepository.GetUserById(userId, userName);
                if (obj != null && obj.ErrorCode == 0 && obj.UserId > 0)
                {
                    context.Items["User"] = obj;
                    CurrentContext.UserId = obj.UserId;
                }
                // attach user to context on successful jwt validation
            }
            catch (Exception ex)
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }

    public static class CurrentContext
    {
        public static int UserId = 0;
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JWTMiddlewareExtensions
    {
        public static IApplicationBuilder UseJWTMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JWTMiddleware>();
        }
    }
}
