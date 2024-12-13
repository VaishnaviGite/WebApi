using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpManagementApi.Model
{
   

        public class Register
        {
           public string UserName { get; set; }
           public string PasswordHash { get; set; }
           public string Email { get; set; }
            public string Role { get; set; }
        }

        public class Result
        {
            public int ErrorCode { get; set; }
            public string ErrorMsg { get; set; }
        }

        public class LoginUser
        {
           public string Email { get; set; }
          public string PasswordHash { get; set; }
        }

        public class ResLoginUser
        {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public string token { get; set; }

        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
       }

       public class CheckHashPass
      {
        public string UserName { get; set; }

      }

      public class ResCheckHashPass
      {
        public string PasswordHash { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }

    }

    public class GetUser 
    {
        public int UserId { get; set; }
        public string Email { get; set; }
    }

    public class ResGetUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public string token { get; set; }

        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
    }



}
