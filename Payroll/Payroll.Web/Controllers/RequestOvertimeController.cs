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
    public class RequestOvertimeController : Controller
    {
        RequestOvertimeService repo;

        int approver = 2;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Approval()
        {
            return View();
        }


        public RequestOvertimeController()
        {
            repo = new RequestOvertimeService();
        }

        [HttpGet]
        public JsonResult GetData()
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repo.GetList(UserId);

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
        public JsonResult Update([FromBody] RequestOvertimeEntity emp)
        {
            int depatID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimaryGroupSid).Value);
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            if (emp.request_overtime_id == 0)
            {
                if (repo.IsExist(UserId, emp.overtime_date))
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { errorMessage = "Duplicate!" });
                }
            }

            emp.employee_id = UserId;
            emp.ref_department_id = depatID;
            emp.approver_id = approver;
            emp.ref_status_id = 1;
            var data = repo.CreateOrUpdate(emp);
            return Json("");
        }

        #region Approval
        [HttpGet]
        public JsonResult ApprovalList()
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);

            var result = repo.GetForApproval(UserId);

            return Json(result);
        }
        [HttpPost]
        public JsonResult Approve([FromBody] RequestOvertimeEntity emp)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);

            var result = repo.Approve(emp.request_overtime_id, emp.approver_remark, UserId);

            return Json(result);
        }
        [HttpPost]
        public JsonResult Disapprove([FromBody] RequestOvertimeEntity emp)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);

            var result = repo.Disapprove(emp.request_overtime_id, emp.approver_remark, UserId);

            return Json(result);
        }
        #endregion
    }
}