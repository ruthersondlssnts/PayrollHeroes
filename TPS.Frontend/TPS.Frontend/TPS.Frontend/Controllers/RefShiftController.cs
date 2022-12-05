using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Services.Interface;

namespace TPS.Frontend.Controllers
{
    public class RefShiftController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
      new CancellationTokenSource();
        private readonly IRefShiftService _client;

        public RefShiftController(IRefShiftService client)
        {
            _client = client;
        }

        public async Task<IActionResult> GetShifts()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.GetShiftsAsync(_cancellationTokenSource.Token, accessToken);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { data = response.Result });
                default:
                    return Json(new { data = "" });
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddOrEdit(string id = "")
        {
            if (id == "")
                return View(new RefShift());
            else
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                var response = await _client.FindShiftAsync(_cancellationTokenSource.Token, accessToken, id);

                switch (response.StatusCode)
                {
                    case Infrastructure.StatusCode.Success:
                        if (response.Result == null)
                            return NotFound();
                        return View(response.Result);
                    default:
                        return BadRequest(response);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOrEditAsync(string id, RefShift model)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            var response = await _client.AddOrEditShiftAsync(_cancellationTokenSource.Token, accessToken, model);
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { isValid = true });
                default:
                    return BadRequest(response);
            }
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            try
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                var response = await _client.DeleteShiftAsync(_cancellationTokenSource.Token, accessToken, id);

                switch (response.StatusCode)
                {
                    case Infrastructure.StatusCode.Success:
                        return RedirectToAction(nameof(Index));
                    default:
                        return BadRequest(response);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
