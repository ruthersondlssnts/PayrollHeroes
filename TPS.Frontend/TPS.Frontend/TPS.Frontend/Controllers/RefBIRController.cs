using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Services.Interface;

namespace TPS.Frontend.Controllers
{
    public class RefBIRController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource =
      new CancellationTokenSource();
        private readonly IRefBIRService _client;

        public RefBIRController(IRefBIRService client)
        {
            _client = client;
        }

        public async Task<IActionResult> GetAllBIR()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.GetAllBIRAsync(_cancellationTokenSource.Token, accessToken);

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
                var response = await _client.FindBIRAsync(_cancellationTokenSource.Token, accessToken, id);

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
        public async Task<ActionResult> AddOrEdit(string id, RefBIR model)
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.AddOrEditBIRAsync(_cancellationTokenSource.Token, accessToken, model);

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
                var response = await _client.DeleteBIRAsync(_cancellationTokenSource.Token, accessToken, id);

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
