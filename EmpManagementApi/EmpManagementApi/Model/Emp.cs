using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpManagementApi.Model
{
  public class ADDEmployee{
       // public int EmpId { get; set; }
        public string EmpName { get; set; }

        public string Department { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public string HireDate{ get; set; }

    }
    public class EmpResult
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
    }
    public class DeleteEmployee
    {
        public int EmpId { get; set; }
    }

    public class ReqGetAllEmp
    {
        public int UserId { get; set; }
    }

    public class ResGetAllEmpList
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }

        public string Department { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public string HireDate { get; set; }

        
    }
    public class ResGetAllEmp
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }

        public string Department { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public string HireDate { get; set; }

        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }


    }

    public class ResGetAllEmpForList
    {
        public List<ResGetAllEmpList>lsdata { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }

    }
    public class ReqGetEmpById
    {
        public int EmpId { get; set; }
    }

    public class UpdateEmployee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }

        public string Department { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public string HireDate { get; set; }

    }

}
