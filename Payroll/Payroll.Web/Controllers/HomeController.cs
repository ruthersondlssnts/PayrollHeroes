using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payroll.Web.Models;

namespace Payroll.Web.Controllers
{
    public class HomeController : Controller
    {
       [Authorize]
        public IActionResult Index()
        {   //Update logs column
            var loggeduser = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var depatID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimaryGroupSid).Value);
            var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
