using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payroll.Service;

namespace Payroll.Web.Controllers
{
    public class PayrollController : Controller
    {
        PayrollService repo;
        RefPayrollCutoffService repopaycut;
        public PayrollController()
        {
            repo = new PayrollService();
            repopaycut = new RefPayrollCutoffService();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetListEarnings(int id)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repo.GetList(UserId, id);

            return Json(result.Where(a => a.ref_payroll_details_type_.earnings && !a.ref_payroll_details_type_.company_contribution));
        }

        [HttpGet]
        public JsonResult GetListDeduction(int id)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repo.GetList(UserId, id);

            return Json(result.Where(a=>!a.ref_payroll_details_type_.earnings && !a.ref_payroll_details_type_.company_contribution));
        }

        [HttpGet]
        public JsonResult GetCutoff()
        {
            var result = repopaycut.
            GetList().Select(x => new
            {
                ref_payroll_cutoff_id = x.ref_payroll_cutoff_id,
                ref_payroll_cutoff_name = x.cutoff_date_start.ToString("MM/dd/yyyy") + " - " + x.cutoff_date_end.ToString("MM/dd/yyyy")
            }).ToList();
            return Json(result);
        }
    }
}