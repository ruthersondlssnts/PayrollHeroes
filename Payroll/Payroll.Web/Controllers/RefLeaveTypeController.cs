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
    public class RefLeaveTypeController : Controller
    {
        RefLeaveTypeService repo;

        public IActionResult Index()
        {
           
            return View();
        }


        public RefLeaveTypeController()
        {
            repo = new RefLeaveTypeService();

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
        public JsonResult Update([FromBody] RefLeaveTypeEntity emp)
        {
            var data = repo.CreateOrUpdate(emp);
            return Json("");
        }
        
    }
}