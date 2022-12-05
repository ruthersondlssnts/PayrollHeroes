using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Services.Interface;

namespace TPS.Frontend.Controllers
{
    public class CalendarController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
new CancellationTokenSource();
        private readonly IRequestLeaveService _client;
        private readonly IEmployeeTimesheetService _dtr;

        public CalendarController(IRequestLeaveService client, IEmployeeTimesheetService dtr)
        {
            _client = client;
            _dtr = dtr;
        }

        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeeDTR()
        {
            return View();
        }

        public async Task<ActionResult> GetLeaves([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var response = await _client.GetAllByApproverIdCalendar(_cancellationTokenSource.Token, token, userId);

            var events = response.Result.Select(x => new
            {
                title = x.FirstName + " " + x.LastName,
                start = x.DateStart.Date,
                end = x.DateEnd.Date,
                status = x.ApprovalStatusName,
                leave = x.LeaveTypeName,
                ishalf = x.IsHalf,
                days = x.LeaveDay,
                isfirsthalf = x.IsFirstHalf
            });
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(events);
                default:
                    return BadRequest(response);
            }
        }
        public async Task<ActionResult> dtr([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var response = await _dtr.FindDtrEmployeeByDate(_cancellationTokenSource.Token, token, userId, start,end);

            var events = response.Result.Select(x => new
            {
                title = x.Title,
                start = x.Start,
                end = x.End,
                content = x.Content,
                absent = x.Absent,
                color = x.Color
            });
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(events);
                default:
                    return BadRequest(response);
            }
        }


        //public async Task<ActionResult> GetDtr()
        //{
        //    var token = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
        //    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

        //    switch (response.StatusCode)
        //    {
        //        case Infrastructure.StatusCode.Success:
        //            return Json(events);
        //        default:
        //            return BadRequest(response);
        //    }
        //}


    }
}
