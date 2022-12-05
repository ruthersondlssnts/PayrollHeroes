using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payroll.Core.Entities;
using Payroll.Service;

namespace Payroll.Web.Controllers
{
    public class RequestDTRController : Controller
    {
        RequestDtrService repo;
        RefShiftService reposhift;

        int approver = 5;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Approval()
        {
            return View();
        }


        public RequestDTRController()
        {
            repo = new RequestDtrService();
            reposhift = new RefShiftService();
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

        [HttpPost]
        public JsonResult Update([FromBody] RequestDTREntity emp)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            if (emp.request_dtr_id == 0)
            {
                if (repo.IsExist(UserId, emp.shift_date))
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { errorMessage = "Duplicate!" });
                }
            }
            emp.employee_id = UserId;
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
        public JsonResult Approve([FromBody] RequestDTREntity emp)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repo.Approve(emp.request_dtr_id, emp.approver_remark, UserId);

            return Json(result);
        }
        [HttpPost]
        public JsonResult Disapprove([FromBody] RequestDTREntity emp)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repo.Disapprove(emp.request_dtr_id, emp.approver_remark, UserId);

            return Json(result);
        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            var result = repo.Delete(id);
            return Json(result);
        }
        #endregion
    }
}