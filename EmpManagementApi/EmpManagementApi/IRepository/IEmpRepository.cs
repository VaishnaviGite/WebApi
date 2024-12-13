using EmpManagementApi.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpManagementApi.IRepository
{
    public interface IEmpRepository
    {
        EmpResult ADDEmployee(ADDEmployee addemployee);
        EmpResult DeleteEmployee(DeleteEmployee deleteEmployee);
        //ResGetAllEmp GetAllEmployee(ReqGetAllEmp reqGetAllEmp);
        List<ResGetAllEmpList> GetAllEmployee(ReqGetAllEmp reqGetAllEmp);

        ResGetAllEmp GetEmployeeById(ReqGetEmpById reqGetEmpById);

        EmpResult UpdateEmployee(UpdateEmployee updateEmployee);
    }
}
