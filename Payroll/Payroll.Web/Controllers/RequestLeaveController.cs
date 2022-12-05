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
    public class RequestLeaveController : Controller
    {
        RequestLeaveService repo;
        RefLeaveTypeService repoLeaveType;
        EmployeeBalanceService repobalance;
        int approver = 2;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Approval()
        {
            return View();
        }


        public RequestLeaveController()
        {
            repo = new RequestLeaveService();
            repoLeaveType = new RefLeaveTypeService();
            repobalance = new EmployeeBalanceService();
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
        public JsonResult Update([FromBody] RequestLeaveEntity emp)
        {
            int depatID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimaryGroupSid).Value);
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            if (emp.request_leave_id == 0)
            {
                if (repo.IsExist(UserId, emp.leave_date))
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { errorMessage = "Duplicate!" });
                }
            }

            //VALIDATE Leave Credits
            DateTime dt = DateTime.Now;
            decimal LeaveCredits = repobalance.GetBalance(emp.leave_date, emp.ref_leave_type_id, UserId);

            if (LeaveCredits < emp.leave_day)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { errorMessage = "Leave credit low!" });
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
        public JsonResult Approve([FromBody] RequestLeaveEntity emp)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repo.Approve(emp.request_leave_id, emp.approver_remark, UserId);

            return Json(result);
        }
        [HttpPost]
        public JsonResult Disapprove([FromBody] RequestLeaveEntity emp)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repo.Disapprove(emp.request_leave_id, emp.approver_remark, UserId);

            return Json(result);
        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            var result = repo.Delete(id);
            return Json(result);
        }
        #endregion

        [HttpGet]
        public JsonResult GetLeaveType()
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);

            var result = repoLeaveType.
            GetList(UserId).Select(x => new
            {
                ref_leave_type_id = x.ref_leave_type_id,
                ref_leave_type_name = x.ref_leave_type_name
            }).ToList();
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetEmployeeBalance()
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repobalance.GetEmployeeBalance(DateTime.Now,UserId);

            return Json(result);
        }
        
    }
}