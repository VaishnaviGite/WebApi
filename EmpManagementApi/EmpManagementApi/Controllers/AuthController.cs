using CryptoHelper;
using EmpManagementApi.IRepository;
using EmpManagementApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmpManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }
        [HttpPost("UserRegister")]
        public Result UserRegister([FromBody] Register register)
        {
            //register.PasswordHash = BCrypt.Net.BCrypt.HashPassword(register.PasswordHash);
            register.PasswordHash = HashPassword(register.PasswordHash);
            var ans = _authRepository.Register(register);
            return ans;

        }

        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginUser loginUser)
        {
            ResLoginUser resLoginUser = new ResLoginUser();
            var Email = loginUser.Email;
            var result = _authRepository.VerifyPass(Email);

            if (result.ErrorCode == 0)
            {
                var PassForVerify = result.PasswordHash;

                //bool passmatch = BCrypt.Net.BCrypt.Verify(loginUser.PasswordHash, PassForVerify);
                bool passmatch = VerifyPassword(PassForVerify, loginUser.PasswordHash);

                if (passmatch)
                {
                    loginUser.PasswordHash = PassForVerify;
                    resLoginUser = _authRepository.LoginUser(loginUser);
                  // var  res = _authRepository.LoginUser(loginUser);


                    if (resLoginUser.ErrorCode == 0)
                    {

                        //var tokenhandler = new JwtSecurityTokenHandler();
                        //var key = Encoding.ASCII.GetBytes(_config["JWT:Key"]);
                        //var tokenDiscriptor = new SecurityTokenDescriptor
                        //{
                        //    Subject = new ClaimsIdentity(new Claim[]
                        //    {
                        //        new Claim(ClaimTypes.Name, resLoginUser.Email)
                        //    }),
                        //    Expires = DateTime.UtcNow.AddMinutes(10),
                        //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
                        //};
                        //var token = tokenhandler.CreateToken(tokenDiscriptor);


                        return Ok(resLoginUser);

                        // var token = GenerateJWTToken(loginUser);

                        //if (string.IsNullOrEmpty(token))
                        //{
                        //    return Unauthorized();
                        //}

                       // return Ok(tokenhandler.WriteToken(token));
                    }

                }
                else
                {
                    resLoginUser.ErrorCode = -1;
                    resLoginUser.ErrorMsg = "Password does not match";
                }
            }
            else
            {
                resLoginUser.ErrorCode = result.ErrorCode;
                resLoginUser.ErrorMsg = result.ErrorMsg;

            }
           // return (IActionResult)resLoginUser;
            return Ok(resLoginUser);



        }

        [ApiExplorerSettings(IgnoreApi =true)]

        // Method for hashing the password
        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }
        // Method to verify the password hash against the given password

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool VerifyPassword(string hash, string password)
        {
            return Crypto.VerifyHashedPassword(hash, password);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private string GenerateJWTToken(LoginUser login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
             new Claim(JwtRegisteredClaimNames.Sub, login.Email), // Subject claim
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique identifier for the token
        // Additional claims as needed
             };

            var token = new JwtSecurityToken
            (
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["JWT:ExpireMinute"])), // Note: changed from ExpireMinutes to ExpireMinute
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
