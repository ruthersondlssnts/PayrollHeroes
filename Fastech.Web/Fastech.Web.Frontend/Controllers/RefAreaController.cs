using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastech.Web.Frontend.Controllers
{
    public class RefAreaController : Controller
    {
        // GET: RefAreaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RefAreaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RefAreaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RefAreaController/Create
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

        // GET: RefAreaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RefAreaController/Edit/5
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

        // GET: RefAreaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RefAreaController/Delete/5
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
