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
    public class EmployeeController : Controller
    {
        EmployeeService repo;
        RefShiftService reposhift;
        RefDepartmentService repoDept;
        RoleService repoRole;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Approval()
        {
            return View();
        }


        public EmployeeController()
        {
            repo = new EmployeeService();
            reposhift = new RefShiftService();
            repoDept = new RefDepartmentService();
            repoRole = new RoleService();
        }

        [HttpGet]
        public JsonResult GetList()
        {
            var result = repo.GetList();

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
        public JsonResult Update([FromBody] EmployeeEntity emp)
        {
            var data = repo.CreateOrUpdate(emp);
            return Json("");
        }
        [HttpGet]
        public JsonResult GetShift()
        {
            var result = reposhift.
            GetList().Select(x => new
            {
                ref_shift_id = x.ref_shift_id,
                shift_name = x.shift_name
            }).ToList();
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetDepartment()
        {
            var result = repoDept.
            GetList().Select(x => new
            {
                ref_department_id = x.ref_department_id,
                department_name = x.department_name
            }).ToList();
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetRole()
        {
            var result = repoRole.
            GetList().Select(x => new
            {
                role_id = x.role_id,
                role_name = x.role_name
            }).ToList();
            return Json(result);
        }
    }
}