using EmpManagementApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpManagementApi.IRepository
{
    public interface IAuthRepository
    {
        Result Register(Register register);

        ResLoginUser LoginUser(LoginUser loginUser);

        ResCheckHashPass VerifyPass(string userName);
        ResGetUser GetUserById(int id, string email);
        string GenerateJSONWebToken(int id, string email);
    }
}
