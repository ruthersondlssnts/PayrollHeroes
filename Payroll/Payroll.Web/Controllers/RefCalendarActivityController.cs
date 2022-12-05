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
    public class RefCalendarActivityController : Controller
    {
        RefCalendarActivityService repo;
    

        public IActionResult Index()
        {
           
            return View();
        }


        public RefCalendarActivityController()
        {
            repo = new RefCalendarActivityService();

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
        public JsonResult Update([FromBody] RefCalendarActivityEntity emp)
        {
            if (repo.Exist((DateTime)emp.work_date) && emp.ref_calendar_activity_id==0)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { errorMessage = "Duplicate!" });
            }

            var data = repo.CreateOrUpdate(emp);
            return Json("");
        }
        [HttpGet]
        public JsonResult GetDetails(int id)
        {

            var result = repo.GetByID(id);

            return Json(result);
        }
        [HttpGet]
        public JsonResult GetDayType()
        {

            var result = repo.GetDayType().Select(x => new
            {
                ref_day_type_id = x.ref_day_type_id,
                date_type_name = x.date_type_name
            }).ToList();
            return Json(result);
        }
    }
}