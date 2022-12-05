using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Services.Interface;

namespace TPS.Frontend.Controllers
{
    public class DashboardController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
 new CancellationTokenSource();
        private readonly IDashboardService _client;
        public DashboardController(IDashboardService client)
        {
            _client = client;
        }
        // GET: DashboardController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> ForApprovals()
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var response = await _client.ForApprovalsAsync(_cancellationTokenSource.Token, token, userId);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(response.Result);
                default:
                    return BadRequest(response);
            }
        }



    }
}
