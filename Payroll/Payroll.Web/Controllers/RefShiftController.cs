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
    public class RefShiftController : Controller
    {
        RefShiftService repo;

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Approval()
        {
            return View();
        }


        public RefShiftController()
        {
            repo = new RefShiftService();
        }

        [HttpGet]
        public JsonResult GetData()
        {
            var result = repo.GetList();

            return Json(result);
        }
        [HttpGet]
        public JsonResult GetDays(int id)
        {
            
            var result = repo.GetShiftDetails(id);

            return Json(result);
        }
        [HttpGet]
        public JsonResult GetDaysEmpty()
        {
            var result = repo.GetDaysEmpty();

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
        public JsonResult Update([FromBody] RefShiftEntity emp)
        {
            //if (emp.ref_shift_id == 0)
            //{
            //    if (repo.Exist(emp.shift_name))
            //    {
            //        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //        return Json(new { responseText = "Something really bad happened" });

            //    }
            //}
            var data = repo.CreateOrUpdate(emp);
            return Json("");
        }
    }
}