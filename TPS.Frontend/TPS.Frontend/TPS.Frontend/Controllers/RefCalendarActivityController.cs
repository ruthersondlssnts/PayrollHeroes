using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Models;
using TPS.Frontend.Services.Interface;
using TPS.Frontend.Helper;
using System.Security.Claims;

namespace TPS.Frontend.Controllers
{
    public class RefCalendarActivityController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
       new CancellationTokenSource();
        private readonly IRefCalendarActivityService _client;
        public RefCalendarActivityController(IRefCalendarActivityService client)
        {
            _client = client;
        }
        public async Task<IActionResult> GetCalendarActivities()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.GetCalendarActivitiesAsync(_cancellationTokenSource.Token, accessToken);
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


        // POST: RefCalendarActivitiesController/Create

        public async Task<IActionResult> AddOrEdit(string id = "")
        {
            if (id == "")
                return View(new RefCalendarActivity());
            else
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                var response = await _client.FindCalendarActivityAsync(_cancellationTokenSource.Token, accessToken, id);

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
        public async Task<ActionResult> AddOrEditAsync(string id, RefCalendarActivity model)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.AddEditCalendarActivityAsync(_cancellationTokenSource.Token, accessToken, model);

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

        // POST: RefCalendarActivitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            try
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                var response = await _client.DeleteCalendarActivityAsync(_cancellationTokenSource.Token, accessToken, id);

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
