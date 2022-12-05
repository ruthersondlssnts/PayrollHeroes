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
    public class RefLeaveTypeController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
             new CancellationTokenSource();
        private readonly IRefLeaveTypeService _client;

        public RefLeaveTypeController(IRefLeaveTypeService client)
        {
            _client = client;
        }

        public async Task<IActionResult> GetLeaveTypes()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.GetLeaveTypesAsync(_cancellationTokenSource.Token, accessToken);

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
                return View(new RefLeaveType());
            else
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                var response = await _client.FindLeaveTypeAsync(_cancellationTokenSource.Token, accessToken, id);

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
        public async Task<IActionResult> AddOrEdit(string id, RefLeaveType model)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.AddOrEditLeaveTypeAsync(_cancellationTokenSource.Token, accessToken, model);

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
        public async Task<IActionResult> Delete(string id)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.DeleteLeaveTypeAsync(_cancellationTokenSource.Token, accessToken, id);

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
