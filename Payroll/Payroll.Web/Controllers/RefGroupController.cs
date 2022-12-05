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
    public class RefGroupController : Controller
    {
        RefGroupService repo;
        EmployeeService repoEmployee;
        EmployeeGroupService repoEmployeeGroup;
        public RefGroupController()
        {
            repo = new RefGroupService();
            repoEmployee = new EmployeeService();
            repoEmployeeGroup = new EmployeeGroupService();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetData(int id)
        {
            if (id == 0) id = 1;
            var result = repo.GetAllList().Where(a=>a.ancestor1 == id);
            //var result = repo.GetList(id);

            return Json(result);
        }

        [HttpGet]
        public JsonResult GetNextVal(int id)
        {
            if (id == 0) id = 1;
            var result = repo.GetAllList().Where(a => a.group_id == id).FirstOrDefault();
            //var result = repo.GetList(id);

            return Json(result.nextval);
        }

        [HttpGet]
        public JsonResult GetInfo(int id)
        {
            if (id == 0) id = 1;
            var result = repo.GetAllList().Where(a => a.group_id == id).FirstOrDefault();
            //var result = repo.GetList(id);

            return Json(result);
        }

        [HttpPost]
        public JsonResult Update([FromBody] RefGroupEntity emp)
        {
            var data = repo.CreateOrUpdate(emp);
            return Json("");
        }

        public JsonResult GetbyID(int id)
        {
            var result = repo.GetByID(id);
            return Json(result);
        }
        [HttpGet]
        public JsonResult Delete(int id)
        {
            var result = repoEmployeeGroup.Delete(id);
            return Json("");
        }

        [HttpGet]
        public JsonResult ValidateEmpExist(int id)
        {
            var result = repoEmployeeGroup.Validate(id);
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetEmployee()
        {
            var result = repoEmployee.GetList();

            return Json(result);
        }

        [HttpGet]
        public JsonResult GetEmployeeList(int id)
        {
            var result = repoEmployeeGroup.GetList(id);

            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateEmpGroup([FromBody] EmployeeGroupEntity emp)
        {
            var data = repoEmployeeGroup.CreateOrUpdate(emp);
            return Json("");
        }

        [HttpGet]
        public JsonResult UpdateApproval(int employee_id, int type, bool value)
        {
            var result = repoEmployeeGroup.UpdateApproval( employee_id,  type,  value);

            return Json("");
        }
    }
}