using EmpManagementApi.Context;
using EmpManagementApi.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmpManagementApi.IRepository
{
    public class AuthRepository : IAuthRepository
    {
        clsDB dbnew = new clsDB(Config.ConStr);

        public string GenerateJSONWebToken(int id, string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MANTRA@InnovationThatCounts"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.NameId, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(null,
             null,
              claims,
              expires: DateTime.Now.AddHours(8),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public ResGetUser GetUserById(int id ,string email)
        {
            ResGetUser res = new ResGetUser();

            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("Id", id);
            p[1] = new SqlParameter("Email", email);

            DataTable dt = dbnew.ExecuteDataTable("SP_GetUserBYID", p);
            int j = dt.Rows.Count - 1;

            if(dt!=null && dt.Rows.Count > 0)
            {
                for(int i = 0; i <= j; i++)
                {
                    if (Convert.ToInt16(dt.Rows[0]["ErrorCode"]) != 0)
                    {
                        res.ErrorCode = Convert.ToInt16(dt.Rows[0]["ErrorCode"]);
                        res.ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();
                    }
                    else
                    {
                        res.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);
                        res.UserName = Convert.ToString(dt.Rows[i]["UserName"]);
                        res.Email = Convert.ToString(dt.Rows[i]["Email"]);
                        res.Role = Convert.ToString(dt.Rows[i]["Role"]);
                        res.ErrorCode = Convert.ToInt16(dt.Rows[0]["ErrorCode"]);
                        res.ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();

                        res.token = GenerateJSONWebToken(res.UserId, res.Email);
                    }
                }
            }
            return res;

        }
        public Result Register(Register register)
        {
            Result result = new Result();

            SqlParameter[] p = new SqlParameter[4];

            p[0] = new SqlParameter("UserName", register.UserName);
            p[1] = new SqlParameter("PasswordHash", register.PasswordHash);
            p[2] = new SqlParameter("Email", register.Email);
            p[3] = new SqlParameter("Role", register.Role);

            DataTable dt = dbnew.ExecuteDataTable("sp_RegisterUser", p);

            int j = dt.Rows.Count - 1;

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i <= j; i++)
                {
                    result.ErrorCode = Convert.ToInt16(dt.Rows[0]["ErrorCode"]);
                    result.ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();
                }
            }

            return result;
        }
        public ResLoginUser LoginUser(LoginUser loginUser)
        {
            ResLoginUser res = new ResLoginUser();

            SqlParameter[] p = new SqlParameter[2];

            p[0] = new SqlParameter("Email", loginUser.Email);
            p[1] = new SqlParameter("PasswordHash", loginUser.PasswordHash);

            DataTable dt = dbnew.ExecuteDataTable("sp_LoginUser", p);
            int j = dt.Rows.Count - 1;

            if (dt != null & dt.Rows.Count > 0)
            {
                for (int i = 0; i <= j; i++)
                {
                    if (Convert.ToInt16(dt.Rows[0]["ErrorCode"]) != 0)
                    {
                        res.ErrorCode = Convert.ToInt16(dt.Rows[0]["ErrorCode"]);
                        res.ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();
                    }
                    else
                    {
                        res.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);
                        res.UserName = Convert.ToString(dt.Rows[i]["UserName"]);
                        res.Email = Convert.ToString(dt.Rows[i]["Email"]);
                        res.Role = Convert.ToString(dt.Rows[i]["Role"]);
                        res.ErrorCode = Convert.ToInt16(dt.Rows[0]["ErrorCode"]);
                        res.ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();

                        res.token = GenerateJSONWebToken(res.UserId, res.Email);
                    }
                }
            }
            return res;
        }

        public ResCheckHashPass VerifyPass(string email)
        {
            ResCheckHashPass res = new ResCheckHashPass();

            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("Email", email);


            DataTable dt = dbnew.ExecuteDataTable("sp_GetHashPassword", p);
            int j = dt.Rows.Count - 1;

            if (dt != null & dt.Rows.Count > 0)
            {
                for (int i = 0; i <= j; i++)
                {

                    if (Convert.ToInt16(dt.Rows[0]["ErrorCode"]) != 0)
                    {
                        res.ErrorCode = Convert.ToInt16(dt.Rows[0]["ErrorCode"]);
                        res.ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();
                    }
                    else
                    {
                        res.PasswordHash = Convert.ToString(dt.Rows[i]["PasswordHash"]);
                        res.ErrorCode = Convert.ToInt16(dt.Rows[0]["ErrorCode"]);
                        res.ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();
                    }
                    
                }

            }
            return res;
        }

    }
}
