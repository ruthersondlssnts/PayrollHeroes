using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Services.Interface;
using static TPS.Frontend.Infrastructure.Enums;

namespace TPS.Frontend.Controllers
{
    public class UserNotificationController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
              new CancellationTokenSource();
        private readonly IUserNotificationService _client;

        public UserNotificationController(IUserNotificationService client)
        {
            _client = client;
        }

        public async Task<IActionResult> GetAllUserNotifications()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.GetAllUserNotificationsAsync(_cancellationTokenSource.Token, accessToken, userId);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { response.Result });
                default:
                    return BadRequest(response);
            }
        }
        public async Task<IActionResult> GetUnreadUserNotifications()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.GetUnreadUserNotificationsAsync(_cancellationTokenSource.Token, accessToken, userId);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { response.Result });
                default:
                    return BadRequest(response);
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddOrEdit(string id = "")
        {
            if (id == "")
                return View(new UserNotification());
            else
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                var response = await _client.FindUserNotificationAsync(_cancellationTokenSource.Token, accessToken, id);

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
        public async Task<IActionResult> AddOrEdit(string id, UserNotification model)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.AddOrEditUserNotificationAsync(_cancellationTokenSource.Token, accessToken, model);

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
            var response = await _client.DeleteUserNotificationAsync(_cancellationTokenSource.Token, accessToken, id);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return RedirectToAction(nameof(Index));
                default:
                    return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteByType(int type)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.DeleteByTypeAsync(_cancellationTokenSource.Token, accessToken, type);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Ok();
                default:
                    return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAllToRead()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.UpdateAllToReadAsync(_cancellationTokenSource.Token, accessToken);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Ok();
                default:
                    return BadRequest(response);
            }
        }
    }
}
