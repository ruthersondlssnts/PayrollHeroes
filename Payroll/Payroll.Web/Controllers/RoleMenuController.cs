using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payroll.Core.Entities;
using Payroll.Service;
using System.Security.Claims;

namespace Payroll.Web.Controllers
{
    public class RoleMenuController : Controller
    {
        RoleMenuService repo;
        RoleService repoRole;
        int approver = 2;
        public IActionResult Index()
        {
            return View();
        }

        public RoleMenuController()
        {
            repo = new RoleMenuService();
            repoRole = new RoleService();
        }
        [HttpGet]
        public JsonResult GetList()
        {
            var result = repo.GetList();

            return Json(result);
        }
        [HttpGet]
        public JsonResult GetData(int id)
        {
            var result = repo.GetList(id);

            return Json(result);
        }
        [HttpGet]
        public JsonResult GetbyID(int id)
        {
            var result = repo.GetByID(id);
            return Json(result);
        }
        [HttpGet]
        public JsonResult Delete(int id)
        {
            var result = repo.Delete(id);
            return Json(result);
        }
        [HttpPost]
        public JsonResult Update([FromBody] RoleMenuEntity emp)
        {
            var data = repo.CreateOrUpdate(emp);
            return Json("");
        }

        [HttpGet]
        public JsonResult GetRoot()
        {

            var result = repo.
            GetList().Where(a=>a.role_menu_parent_id==0).Select(x => new
            {
                x.role_menu_id,
                x.display_name
            }).ToList();
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetRole()
        {
            var result = repoRole.
            GetList().Select(x => new
            {
                role_id = x.role_id,
                role_name = x.role_name
            }).ToList();
            return Json(result);
        }
    }
}