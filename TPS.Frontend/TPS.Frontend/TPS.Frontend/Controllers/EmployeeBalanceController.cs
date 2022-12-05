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
    public class EmployeeBalanceController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
                  new CancellationTokenSource();
        private readonly IEmployeeBalanceService _client;

        public EmployeeBalanceController(IEmployeeBalanceService client)
        {
            _client = client;
        }
        public async Task<IActionResult> GetEmployeeLeaveTypeBalance(string leaveTypeId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.FindEmployeeLeaveTypeBalanceAsync(_cancellationTokenSource.Token, accessToken, userId, leaveTypeId);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(response.Result);
                default:
                    return BadRequest(response);
            }
        }
        public async Task<IActionResult> GetEmployeeBalances()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.GetEmployeeBalancesAsync(_cancellationTokenSource.Token, accessToken);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { data = response.Result });
                case Infrastructure.StatusCode.Bad_Request:
                    return BadRequest(response);
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
                return View(new EmployeeBalance());
            else
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                var response = await _client.FindEmployeeBalanceAsync(_cancellationTokenSource.Token, accessToken, id);

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
        public async Task<ActionResult> AddOrEdit(string id, EmployeeBalance model)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.AddEditEmployeeBalanceAsync(_cancellationTokenSource.Token, accessToken, model);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { isValid = true });
                case Infrastructure.StatusCode.Conflict:
                case Infrastructure.StatusCode.DUPLICATE:
                    return Json(new { isValid = false, html = Helper.Helper.RenderRazorViewToString(this, "AddOrEdit", model), error = response.Message });
                default:
                    return BadRequest(response);
            }
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.DeleteEmployeeBalanceAsync(_cancellationTokenSource.Token, accessToken, id);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return RedirectToAction(nameof(Index));
                default:
                    return BadRequest(response);
            }
        }
    }
}
