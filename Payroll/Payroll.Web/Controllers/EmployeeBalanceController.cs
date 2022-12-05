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
    public class EmployeeBalanceController : Controller
    {
        EmployeeBalanceService repo;
        RefLeaveTypeService repoLeaveType;
        EmployeeService repoEmployee;
        public IActionResult Index()
        {
           
            return View();
        }


        public EmployeeBalanceController()
        {
            repo = new EmployeeBalanceService();
            repoLeaveType = new RefLeaveTypeService();
            repoEmployee = new EmployeeService();
        }

        [HttpGet]
        public JsonResult GetList()
        {
            var result = repo.GetList();

            return Json(result);
        }
        [HttpGet]
        public JsonResult GetEmployeeBalance()
        {
            DateTime dt = DateTime.Now;
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);

            var result = repo.GetEmployeeBalance(dt, UserId);

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
        public JsonResult Update([FromBody] EmployeeBalanceEntity emp)
        {
            var data = repo.CreateOrUpdate(emp);
            return Json("");
        }

        [HttpGet]
        public JsonResult GetLeaveType()
        {
            var result = repoLeaveType.
            GetList().Select(x => new
            {
                ref_leave_type_id = x.ref_leave_type_id,
                ref_leave_type_name = x.ref_leave_type_name
            }).ToList();
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetEmployee()
        {
            var result = repoEmployee.
            GetList().Select(x => new
            {
                employee_id = x.employee_id,
                name = x.last_name + ", " + x.first_name
            }).ToList();
            return Json(result);
        }
    }
}