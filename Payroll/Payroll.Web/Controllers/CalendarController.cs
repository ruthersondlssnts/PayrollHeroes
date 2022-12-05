using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payroll.Service;
using System.Runtime;
namespace Payroll.Web.Controllers
{
    public class CalendarController : Controller
    {
        RequestLeaveService repo;
        RefCalendarActivityService repoCalendar;
        public IActionResult Index()
        {
            return View();
        }

        public CalendarController()
        {
            repo = new RequestLeaveService();
            repoCalendar = new RefCalendarActivityService();
        }

        [HttpGet]
        public JsonResult GetData()
        {

            var result_leave = repo.GetList();
            var result_holiday = repoCalendar.GetList();
            List<CalendarEntity> result = new List<CalendarEntity>();

            foreach (var row in result_leave)
            {
                CalendarEntity objNew = new CalendarEntity();
                objNew.id = (int)row.request_leave_id;
                objNew.reason = row.reason;
                objNew.name = row.employee_.last_name + ", " + row.employee_.first_name;
                objNew.ref_leave_type_code = row.ref_leave_type_.ref_leave_type_code;
                objNew.dtStart = row.leave_date;
                objNew.dtEnd = row.leave_date;
                objNew.ref_status_id = row.ref_status_id;
                result.Add(objNew);
            }

            foreach (var row in result_holiday)
            {
                CalendarEntity objNew = new CalendarEntity();
                objNew.id = 0;
                objNew.name = row.description;
                objNew.reason = "";
                objNew.ref_leave_type_code = row.ref_day_type_.date_type_name;
                objNew.dtStart = (DateTime)row.work_date;
                objNew.dtEnd = (DateTime)row.work_date;
                objNew.ref_status_id = 0;
                result.Add(objNew);
            }

            return Json(result);
        }


    }

    public class CalendarEntity
    {
       public string ref_leave_type_code { get; set; }
      public int  ref_status_id { get; set; }
        //request_leave_id
        public int id { get; set; }

        public DateTime dtStart { get; set; }
        public DateTime dtEnd { get; set; }
        public string reason { get; set; }
        public string name { get; set; }
    }
}