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
    public class RefGroupController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
      new CancellationTokenSource();
        private readonly IRefGroupService _client;

        public RefGroupController(IRefGroupService client)
        {
            _client = client;
        }

        // GET: RefGroupController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetGroupDescendants(string pathToNode)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            var response = await _client.GetGroupDescendantsAsync(_cancellationTokenSource.Token, accessToken, pathToNode);
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { data = response.Result });
                default:
                    return Json(new { data = "" });
            }

        }

        public async Task<IActionResult> GetGroups()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            var response = await _client.GetGroupsAsync(_cancellationTokenSource.Token, accessToken);
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(response.Result);
                default:
                    return BadRequest(response);
            }

        }
        public async Task<IActionResult> GetGroupApprovers(string id, string type)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.GetGroupApproversAsync(_cancellationTokenSource.Token, accessToken, id, type);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { data = response.Result });
                default:
                    return Json(new { data = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddApprover(GroupApprover groupApprover)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var groupResponse = await _client.FindGroupAsync(_cancellationTokenSource.Token, accessToken, groupApprover.GroupId);


            switch (groupApprover.Type)
            {
                case "LEAVE":
                    groupResponse.Result.LeaveApprover.Add(groupApprover);
                    break;
                case "DTR":
                    groupResponse.Result.DTRApprover.Add(groupApprover);
                    break;
                case "OVERTIME":
                    groupResponse.Result.OvertimeApprover.Add(groupApprover);
                    break;
                default:
                    break;
            }
            var response = await _client.AddOrEditGroupAsync(_cancellationTokenSource.Token, accessToken, groupResponse.Result);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { isValid = true });
                default:
                    return BadRequest(response);
            }
        }
        [HttpGet]
        public IActionResult AddApprover(string type, string groupId)
        {
            return View(new GroupApprover { Type = type, GroupId = groupId });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteApprover([FromQuery] string type, [FromQuery] string userId, [FromQuery] string groupId)
        {
            try
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

                var groupResponse = await _client.FindGroupAsync(_cancellationTokenSource.Token, accessToken, groupId);
                switch (type)
                {
                    case "LEAVE":
                        groupResponse.Result.LeaveApprover.Remove(groupResponse.Result.LeaveApprover.Find(i => i.ApproverUserId == userId));
                        break;
                    case "DTR":
                        groupResponse.Result.DTRApprover.Remove(groupResponse.Result.DTRApprover.Find(i => i.ApproverUserId == userId));
                        break;
                    case "OVERTIME":
                        groupResponse.Result.OvertimeApprover.Remove(groupResponse.Result.OvertimeApprover.Find(i => i.ApproverUserId == userId));
                        break;
                    default:
                        break;
                }
                var response = await _client.AddOrEditGroupAsync(_cancellationTokenSource.Token, accessToken, groupResponse.Result);

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

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(string pathToNode = null, string id = "")
        {
            if (id == "")
                return View(new RefGroup { PathToNode = pathToNode });
            else
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

                var response = await _client.FindGroupAsync(_cancellationTokenSource.Token, accessToken, id);

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

        // POST: RefGroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(string id, RefGroup model)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            var response = await _client.AddOrEditGroupAsync(_cancellationTokenSource.Token, accessToken, model);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { isValid = true });
                default:
                    return BadRequest(response);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

                var response = await _client.DeleteGroupAsync(_cancellationTokenSource.Token, accessToken, id);
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
