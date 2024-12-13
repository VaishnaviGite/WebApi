using EmpManagementApi.Caching;
using EmpManagementApi.IRepository;
using EmpManagementApi.Model;
using EmpManagementApi.Repository;
using LazyCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmpRepository _empRepository;
        private ICacheProvider _cacheProvider;


        public EmployeeController(IEmpRepository empRepository, ICacheProvider cacheProvider)
        {
            _empRepository = empRepository;
            _cacheProvider = cacheProvider;
        }

       

        [HttpPost("ADDEmployee")]
        public IActionResult ADDEmployee(ADDEmployee addemployee)
        {
            if (addemployee == null)
            {
                return BadRequest("Employee Data is null");
            }
            try
            {
                var empresult = _empRepository.ADDEmployee(addemployee);
                if (empresult.ErrorCode == 0)
                {
                    return Ok(empresult);
                }
                else
                {
                    return BadRequest(empresult);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("DeleteEmployee")]
        public IActionResult DeleteEmployee(DeleteEmployee deleteEmployee)
        {
            
            try
            {
                var empresult = _empRepository.DeleteEmployee(deleteEmployee);
                if (empresult.ErrorCode == 0)
                {
                    return Ok(empresult);
                }
                else
                {
                    return BadRequest(empresult);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("GetAllEmployee")]
        public IActionResult GetAllEmployee(ReqGetAllEmp reqGetAllEmp)
        {

            try
            {
                //In Memory cache 
               // ResGetAllEmp resGetAllEmp = new ResGetAllEmp();
                if (!_cacheProvider.TryGetValue(CacheKeys.Employee,out List<ResGetAllEmpList> resGetAllEmp))
                {
                    resGetAllEmp= _empRepository.GetAllEmployee(reqGetAllEmp);

                    var cacheEntryOption = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(2),
                        SlidingExpiration = TimeSpan.FromMinutes(1),
                        Size = 1024
                    };

                    _cacheProvider.Set(CacheKeys.Employee, resGetAllEmp, cacheEntryOption);

                }
                return Ok(resGetAllEmp);
              
                //var empresult = _empRepository.GetAllEmployee(reqGetAllEmp);
                //if (empresult.ErrorCode == 0)
                //{
                //    return Ok(empresult);
                //}
                //else
                //{
                //    return BadRequest(empresult);
                //}
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("GetEmployeeById")]
        public IActionResult GetEmployeeById(ReqGetEmpById reqGetEmpById)
        {

            try
            {
                var empresult = _empRepository.GetEmployeeById(reqGetEmpById);
                if (empresult.ErrorCode == 0)
                {
                    return Ok(empresult);
                }
                else
                {
                    return BadRequest(empresult);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("UpdateEmployee")]
        public IActionResult UpdateEmployee(UpdateEmployee updateEmployee)
        {

            try
            {
                var empresult = _empRepository.UpdateEmployee(updateEmployee);
                if (empresult.ErrorCode == 0)
                {
                    return Ok(empresult);
                }
                else
                {
                    return BadRequest(empresult);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
