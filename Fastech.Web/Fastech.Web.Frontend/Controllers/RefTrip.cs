using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastech.Web.Frontend.Controllers
{
    public class RefTrip : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
