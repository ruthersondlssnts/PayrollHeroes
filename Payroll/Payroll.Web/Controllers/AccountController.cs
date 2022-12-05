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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Payroll.Web.Controllers
{
    public class AccountController : Controller
    {
        EmployeeService repo;
        RoleMenuService repomenuservice;
        public AccountController()
        {
            repo = new EmployeeService();
            repomenuservice = new RoleMenuService();
        }

        public IActionResult Login()
        {
            string var = Url.Action("Index", "Home");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public IActionResult Validate([FromBody] EmployeeModelEntity user)
        {
            string access = "";
            var data = repo.Login(user.username, user.password, out access);

            if (data == null)
            {
                return Json(new { status = false, message = "Invalid Account!" });
            }
            if (access.Length == 0)
            {
                return Json(new { status = false, message = "No access defined!" });
            }


            UpdateIdentity(data, access);
            return Json(new { status = true, message = "Login Successfull!" });
        }

        public void UpdateIdentity(EmployeeEntity emp, string access)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, emp.username));
            claims.Add(new Claim(ClaimTypes.PrimarySid, emp.employee_id.ToString()));
            claims.Add(new Claim(ClaimTypes.PrimaryGroupSid, emp.ref_department_id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, access));
            claims.Add(new Claim(ClaimTypes.Sid, emp.role_id.ToString()));

            //ROLE MENU
            //Create Session with complex object   
            var role_menu = repomenuservice.GetList(emp.role_id);
            HttpContext.Session.SetComplexData("role_menu", role_menu);

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties();
            props.IsPersistent = true;
            props.ExpiresUtc = DateTime.UtcNow.AddMinutes(16);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);


        }

        [HttpGet]
        public ActionResult ExtendSession()
        {
            string loggeduser = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            int ref_department_id = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimaryGroupSid).Value);
            int role_id = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            string access = Convert.ToString(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value);

            //ROLE MENU
            //Create Session with complex object   
            var role_menu = HttpContext.Session.GetComplexData<List<RoleMenuEntity>>("role_menu");
            HttpContext.Session.SetComplexData("role_menu", role_menu);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, loggeduser));
            claims.Add(new Claim(ClaimTypes.PrimarySid, userId.ToString()));
            claims.Add(new Claim(ClaimTypes.PrimaryGroupSid, ref_department_id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, access));


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties();
            props.IsPersistent = true;
            props.ExpiresUtc = DateTime.UtcNow.AddMinutes(16);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);



            var data = new { IsSuccess = true };
            return Json(data);
        }
    }

    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}

