using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Services.Interface;

namespace TPS.Frontend.Controllers
{
    public class RefPagibigController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
      new CancellationTokenSource();
        private readonly IRefPagibigService _client;

        public RefPagibigController(IRefPagibigService client)
        {
            _client = client;
        }

        public async Task<IActionResult> GetAllPagibig()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.GetAllPagibigAsync(_cancellationTokenSource.Token, accessToken);

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
                return View(new RefBIR());
            else
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                var response = await _client.FindPagibigAsync(_cancellationTokenSource.Token, accessToken, id);

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
        public async Task<ActionResult> AddOrEdit(string id, RefPagibig model)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.AddOrEditPagibigAsync(_cancellationTokenSource.Token, accessToken, model);

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
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
                var response = await _client.DeletePagibigAsync(_cancellationTokenSource.Token, accessToken, id);

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
