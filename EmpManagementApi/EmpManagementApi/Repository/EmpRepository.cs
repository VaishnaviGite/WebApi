using EmpManagementApi.Context;
using EmpManagementApi.IRepository;
using EmpManagementApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmpManagementApi.Repository
{
    public class EmpRepository:IEmpRepository
    {
        clsDB dbnew = new clsDB(Config.ConStr);

        public EmpResult ADDEmployee(ADDEmployee addemployee)
        {
            EmpResult empResult = new EmpResult();

            SqlParameter[] p = new SqlParameter[5];

            p[0] = new SqlParameter("EmpName", addemployee.EmpName);
            p[1] = new SqlParameter("Department", addemployee.Department);
            p[2] = new SqlParameter("Position", addemployee.Position);
            p[3] = new SqlParameter("Salary", addemployee.Salary);
            DateTime hiredate = DateTime.ParseExact(addemployee.HireDate, "dd/MM/yyyy", null);
            p[4] = new SqlParameter("HireDate", hiredate);

            DataTable dt = dbnew.ExecuteDataTable("sp_AddEmployee", p);
            int j = dt.Rows.Count - 1;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i <= j; i++)
                {
                    empResult.ErrorCode = Convert.ToInt16(dt.Rows[0]["ErrorCode"]);
                    empResult.ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();
                }
            }

            return empResult;

        }

        public EmpResult DeleteEmployee(DeleteEmployee deleteEmployee)
        {
            EmpResult empResult = new EmpResult();

            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("EmpName", deleteEmployee.EmpId);
       

            DataTable dt = dbnew.ExecuteDataTable("sp_DeleteEmployee", p);
            int j = dt.Rows.Count - 1;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i <= j; i++)
                {
                    empResult.ErrorCode = Convert.ToInt16(dt.Rows[0]["ErrorCode"]);
                    empResult.ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();
                }
            }

            return empResult;
        }

        public List<ResGetAllEmpList> GetAllEmployee(ReqGetAllEmp reqGetAllEmp)
        {
            ResGetAllEmpForList resGetAllEmp1 = new ResGetAllEmpForList();
            List<ResGetAllEmpList> getAllEmpsList = new List<ResGetAllEmpList>();

            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("UserId", reqGetAllEmp.UserId);
           

            DataTable dt = dbnew.ExecuteDataTable("sp_GetAllEmployees", p);
            int j = dt.Rows.Count - 1;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i <dt.Rows.Count; i++)
                {
                    ResGetAllEmpList resGetAllEmp = new ResGetAllEmpList();
                   
                       resGetAllEmp.EmpId = Convert.ToInt16(dt.Rows[i]["EmpId"]);
                        resGetAllEmp.EmpName = Convert.ToString(dt.Rows[i]["EmpName"]);
                        resGetAllEmp.Department = Convert.ToString(dt.Rows[i]["Department"]);
                        resGetAllEmp.Position = Convert.ToString(dt.Rows[i]["Position"]);
                        resGetAllEmp.Salary = (int)Convert.ToInt64(dt.Rows[i]["Salary"]);
                        resGetAllEmp.HireDate = Convert.ToString(dt.Rows[i]["HireDate"]);

                    getAllEmpsList.Add(resGetAllEmp);
                }
                resGetAllEmp1.lsdata = getAllEmpsList;
                resGetAllEmp1.ErrorCode = 0;
                resGetAllEmp1.ErrorMsg = "success";


            }
            else
            {
                resGetAllEmp1.ErrorCode = -301;
                resGetAllEmp1.ErrorMsg = "Data Not found";
              
            }

            //return resGetAllEmp;
            return getAllEmpsList;

        }

        public ResGetAllEmp GetEmployeeById(ReqGetEmpById reqGetEmpById)
        {
            ResGetAllEmp resGetAllEmp = new ResGetAllEmp();

            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("EmpId", reqGetEmpById.EmpId);


            DataTable dt = dbnew.ExecuteDataTable("sp_GetEmployeeById", p);
            int j = dt.Rows.Count - 1;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i <= j; i++)
                {
                    if (resGetAllEmp.ErrorCode != 0)
                    {
                        resGetAllEmp.ErrorCode = Convert.ToInt16(dt.Rows[0]["ErrorCode"]);
                        resGetAllEmp.ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();
                    }
                    else
                    {
                       // resGetAllEmp.EmpId = Convert.ToInt16(dt.Rows[0]["EmpId"]);
                        resGetAllEmp.EmpName = Convert.ToString(dt.Rows[0]["EmpName"]);
                        resGetAllEmp.Department = Convert.ToString(dt.Rows[0]["Department"]);
                        resGetAllEmp.Position = Convert.ToString(dt.Rows[0]["Position"]);
                        resGetAllEmp.Salary = Convert.ToInt16(dt.Rows[0]["Salary"]);
                        resGetAllEmp.HireDate = Convert.ToString(dt.Rows[0]["HireDate"]);
                    }
                }
            }

            return resGetAllEmp;

        }

        public EmpResult UpdateEmployee(UpdateEmployee updateEmployee)
        {
            EmpResult empResult = new EmpResult();

            SqlParameter[] p = new SqlParameter[6];

            p[0] = new SqlParameter("EmpName", updateEmployee.EmpName);
            p[1] = new SqlParameter("Department", updateEmployee.Department);
            p[2] = new SqlParameter("Position", updateEmployee.Position);
            p[3] = new SqlParameter("Salary", updateEmployee.Salary);

            DateTime hiredate = DateTime.ParseExact(updateEmployee.HireDate, "dd/MM/yyyy", null);
            p[4] = new SqlParameter("HireDate", hiredate);
            p[5] = new SqlParameter("EmpId", updateEmployee.EmpId);

            DataTable dt = dbnew.ExecuteDataTable("sp_UpdateEmployee", p);
            int j = dt.Rows.Count - 1;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i <= j; i++)
                {
                    empResult.ErrorCode = Convert.ToInt16(dt.Rows[0]["ErrorCode"]);
                    empResult.ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();
                }
            }

            return empResult;

        }
    }
}
