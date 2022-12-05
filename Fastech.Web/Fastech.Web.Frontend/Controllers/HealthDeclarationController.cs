using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastech.Web.Frontend.Controllers
{
    public class HealthDeclarationController : Controller
    {
        // GET: HealthDeclarationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HealthDeclarationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HealthDeclarationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HealthDeclarationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HealthDeclarationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HealthDeclarationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HealthDeclarationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HealthDeclarationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
