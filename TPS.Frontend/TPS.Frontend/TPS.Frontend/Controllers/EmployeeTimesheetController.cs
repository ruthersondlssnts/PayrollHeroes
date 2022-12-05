using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Helper;
using TPS.Frontend.Infrastructure.DTO;
using TPS.Frontend.Services.Interface;
using static TPS.Frontend.Infrastructure.Enums;

namespace TPS.Frontend.Controllers
{
    public class EmployeeTimesheetController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
            new CancellationTokenSource();
        private readonly IEmployeeTimesheetService _repoEmployeeTimesheetService;
        private readonly IRefPayrollCutOffService _repoRefPayrollCutoff;

        public EmployeeTimesheetController(IEmployeeTimesheetService repoEmployeeTimesheetService, IRefPayrollCutOffService repoRefPayrollCutoff)
        {
            _repoEmployeeTimesheetService = repoEmployeeTimesheetService;
            _repoRefPayrollCutoff = repoRefPayrollCutoff;
        }

        // GET: EmployeeTimesheetController
        public ActionResult Index()
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            return View();
        }

        public ActionResult Process()
        {
            return View();
        }
        public ActionResult TimesheetReport()
        {
            return View();
        }
        // GET: EmployeeTimesheetController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeTimesheetController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeTimesheetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeTimesheetController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeTimesheetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeTimesheetController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeTimesheetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCutoff()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            var result = await _repoRefPayrollCutoff.GetPayrollCutoffsAsync(_cancellationTokenSource.Token, accessToken);

            var returnResult = result.Result.Select(x => new
            {
                id = x.Id.ToString(),
                name = x.CutoffStartDate.Value.ToString("MM/dd/yyyy") + " - " + x.CutoffEndDate.Value.ToString("MM/dd/yyyy")
            }).ToList();

            return Json(returnResult);
        }

        [HttpGet]
        public JsonResult GetData([FromQuery] string cutoffId)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var result = _repoEmployeeTimesheetService.GetTimesheetByEmployeeId(_cancellationTokenSource.Token, accessToken, cutoffId, UserId);

            if (result.Result.Result == null)
                return Json(new { data = "" });
            else
                return Json(new { data = result.Result.Result });

        }

        [HttpGet]
        public ActionResult GenerateTimesheet(string cutoffId)
        {
            DTOPayrollCutoffRequest model = new DTOPayrollCutoffRequest();
            model.PayrollCutoff = cutoffId;
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            _repoEmployeeTimesheetService.GenerateTimesheet(_cancellationTokenSource.Token, accessToken, model);
            return Json("Success");
        }

        [HttpGet]
        public ActionResult ProcessTimesheet(string cutoffId)
        {
            DTOPayrollCutoffRequest model = new DTOPayrollCutoffRequest();
            model.PayrollCutoff = cutoffId;
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            _repoEmployeeTimesheetService.GenerateTimesheetOthers(_cancellationTokenSource.Token, accessToken, model);
            return Json("Success");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                string directory = @"C:\Uploads\In-Progress\";
                // Determine whether the directory exists.
                if (!Directory.Exists(directory))
                {
                    DirectoryInfo di = Directory.CreateDirectory(directory);
                }
                string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(file.FileName);
                string filePath = directory + filename;
                //await file.CopyToAsync(new FileStream(filePath, FileMode.Create));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                await _repoEmployeeTimesheetService.UploadBiometricsFile(_cancellationTokenSource.Token, accessToken, filename);

                return Json("Success");
            }
            catch (Exception)
            {
                return BadRequest("Server Error: Try again later");
            }

        }
    }
}
