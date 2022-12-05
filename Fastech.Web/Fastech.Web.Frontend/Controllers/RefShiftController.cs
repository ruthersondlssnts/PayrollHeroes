using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastech.Web.Frontend.Controllers
{
    public class RefShiftController : Controller
    {
        // GET: RefShiftController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RefShiftController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RefShiftController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RefShiftController/Create
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

        // GET: RefShiftController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RefShiftController/Edit/5
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

        // GET: RefShiftController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RefShiftController/Delete/5
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
