using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Infrastructure.DTO;
using TPS.Frontend.Infrastructure.Pagination;
using TPS.Frontend.Services.Interface;
using static TPS.Frontend.Infrastructure.Enums;

namespace TPS.Frontend.Controllers
{
    public class RequestLeaveController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
   new CancellationTokenSource();
        private readonly IRequestLeaveService _client;
        private readonly IEmployeeService _employeeService;
        public RequestLeaveController(IRequestLeaveService client, IEmployeeService employeeService)
        {
            _client = client;
            _employeeService = employeeService;
        }

        public async Task<ActionResult> ProcessApproval(string id = "")
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.FindRequestLeaveAsync(_cancellationTokenSource.Token, accessToken, id);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    Enum.TryParse(response.Result.ApprovalStatusName, out ApprovalStatus status);
                    return View(new DTOApproval { ApproverUserId = userId, Id = id, ApprovalStatus = status });
                default:
                    return BadRequest(response);
            }

        }

        [HttpPost]
        public async Task<ActionResult> ProcessApproval(DTOApproval model)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.ProcessApprovalAsync(_cancellationTokenSource.Token, accessToken, model);
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { isValid = true });
                case Infrastructure.StatusCode.Conflict:
                case Infrastructure.StatusCode.DUPLICATE:
                    return Json(new { isValid = false, html = Helper.Helper.RenderRazorViewToString(this, "ProcessApproval", model), error = response.Message });
                default:
                    return BadRequest(response);
            }
        }
        public ActionResult Approvals()
        {
            return View();
        }

        public async Task<IActionResult> GetLeaveApprovals()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var response = await _client.GetLeaveApprovalsAsync(_cancellationTokenSource.Token, accessToken, userId);
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { data = response.Result });
                default:
                    return Json(new { data = "" });
            }
        }

        public async Task<IActionResult> GetUserRequestsLeave()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            //Server Side Parameter
            var draw = Request.Form["draw"].FirstOrDefault(); //The Number of times the API is called for the current datatable.
            var start = Request.Form["start"].FirstOrDefault(); //The count of records to skip. This will be used while Paging in EFCore
            var length = Request.Form["length"].FirstOrDefault(); //Essentially the page size. You can see the Top Dropdown in the Jquery Datatable that says, ‘Showing n entries’. n is the page size.
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(); //The Column that is set for sorting
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();//Ascending / Descending
            var searchValue = Request.Form["search[value]"].FirstOrDefault(); //The Value from the Search Box
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int pageNumber = skip / pageSize + 1;

            PaginatedRequest paginatedRequest = new PaginatedRequest
            {
                OrderByASCOrDESC = sortColumnDirection,
                OrderByColumn = sortColumn,
                SearchKeyword = searchValue,
                PageSize = pageSize,
                PageNumber = pageNumber,
                EmployeeId = userId
            };




            var response = await _client.GetPaginatedRequestLeaveAsync(_cancellationTokenSource.Token, accessToken, paginatedRequest);
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { draw = draw, recordsFiltered = response.TotalRecord, recordsTotal = response.TotalRecord, data = response.DataList });
                default:
                    return Json(new { draw = draw, recordsFiltered = 0, recordsTotal = 0, data = "" });
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddOrEdit(string id = "")
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            if (id == "")
            {
                var response = await _employeeService.FindEmployeeAsync(_cancellationTokenSource.Token, accessToken, userId);
                switch (response.StatusCode)
                {
                    case Infrastructure.StatusCode.Success:
                        return View(new RequestLeave
                        {
                            EmployeeId = userId,
                            IsFirstHalf = true,
                            FirstName = response.Result.FirstName,
                            LastName = response.Result.LastName,
                            GroupName = response.Result.GroupName,
                            GroupId = response.Result.GroupId
                        });
                    default:
                        return BadRequest(response);
                }

            }
            else
            {

                var response = await _client.FindRequestLeaveAsync(_cancellationTokenSource.Token, accessToken, id);

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
        public async Task<ActionResult> AddOrEdit(string id, RequestLeave model)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.AddOrEditRequestLeaveAsync(_cancellationTokenSource.Token, accessToken, model);

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
            try
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

                var response = await _client.DeleteRequestLeaveAsync(_cancellationTokenSource.Token, accessToken, id);
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
