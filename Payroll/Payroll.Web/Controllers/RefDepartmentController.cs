using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payroll.Core.Entities;
using Payroll.Service;
using System.Security.Claims;

namespace Payroll.Web.Controllers
{
    public class RefDepartmentController : Controller
    {
        RefDepartmentService repo;
        EmployeeService repoEmployee;
        public IActionResult Index()
        {
           
            return View();
        }


        public RefDepartmentController()
        {
            repo = new RefDepartmentService();
            repoEmployee = new EmployeeService();
        }

        [HttpGet]
        public JsonResult GetList()
        {
            var result = repo.GetList();

            return Json(result);
        }
        [HttpGet]
        public JsonResult GetApproverList(int deptId)
        {
            var result = repoEmployee.GetList().Select(x => new
            {
                employee_id = x.employee_id,
                name = x.last_name + ", " + x.first_name
            }); ;

            return Json(result);
        }
        [HttpGet]
        public JsonResult GetbyID(int id)
        {
            var result = repo.GetByID(id);
            return Json(result);
        }
        [HttpGet]
        public JsonResult Delete(int id)
        {
            var result = repo.Delete(id);
            return Json(result);
        }
        [HttpPost]
        public JsonResult Update([FromBody] RefDepartmentEntity emp)
        {
            var data = repo.CreateOrUpdate(emp);
            return Json("");
        }
        [HttpGet]
        public JsonResult GetApprover(int id)
        {
            var result = repo.GetApproverList(id);

            if (result != null)
            {
                var returnVal = result.Select(x => new
                {
                    employee_id = x.employee_id,
                    name = x.employee_.last_name + ", " + x.employee_.first_name
                });

                return Json(returnVal);
            }
            return Json(null);
        }
    }
}