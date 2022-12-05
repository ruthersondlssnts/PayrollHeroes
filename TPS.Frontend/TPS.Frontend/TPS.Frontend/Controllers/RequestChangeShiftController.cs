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
    public class RequestChangeShiftController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
    new CancellationTokenSource();
        private readonly IRequestChangeShiftService _client;
        public RequestChangeShiftController(IRequestChangeShiftService client)
        {
            _client = client;
        }
        public async Task<IActionResult> GetChangeShiftApprovals()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var response = await _client.GetChangeShiftApprovalsAsync(_cancellationTokenSource.Token, accessToken, userId);
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { data = response.Result });
                default:
                    return Json(new { data = "" });
            }
        }
        public ActionResult Approval()
        {
            return View();
        }
        public async Task<IActionResult> GetUserRequestsOvertime()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var result = await _client.GetUserRequestsChangeShiftAsync(_cancellationTokenSource.Token, accessToken, userId);
            switch (result.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(result.Result);
                default:
                    return BadRequest(result);
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddOrEdit(string id = "")
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (id == "")
                return View(new RequestChangeShift { EmployeeId = userId, ApproverName = "sample", ApproverUserId = "sample" });
            else
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

                var result = await _client.FindRequestChangeShiftAsync(_cancellationTokenSource.Token, accessToken, id);

                switch (result.StatusCode)
                {
                    case Infrastructure.StatusCode.Success:
                        if (result.Result == null)
                            return NotFound();
                        return View(result.Result);
                    default:
                        return BadRequest(result);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOrEdit(string id, RequestChangeShift model)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var result = await _client.AddOrEditRequestChangeShiftAsync(_cancellationTokenSource.Token, accessToken, model);

            switch (result.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { isValid = true });
                case Infrastructure.StatusCode.Conflict:
                case Infrastructure.StatusCode.DUPLICATE:
                    return Json(new { isValid = false, html = Helper.Helper.RenderRazorViewToString(this, "AddOrEdit", model), error = result.Message });
                default:
                    return BadRequest(result);
            }
        }
        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

                var result = await _client.DeleteRequestChangeShiftAsync(_cancellationTokenSource.Token, accessToken, id);
                switch (result.StatusCode)
                {
                    case Infrastructure.StatusCode.Success:
                        return RedirectToAction(nameof(Index));
                    default:
                        return BadRequest(result);
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
