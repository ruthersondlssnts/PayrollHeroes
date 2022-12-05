using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure.DTO;
using TPS.Frontend.Infrastructure.Models;
using TPS.Frontend.Services.Interfaces;
using static TPS.Frontend.Infrastructure.Enums;
using TPS.Frontend.Helper;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading;

namespace TPS.Frontend.Controllers
{
    public class AccountController : Controller
    {
        private IUserManagerService _userManager;
        private readonly CancellationTokenSource _cancellationTokenSource =
             new CancellationTokenSource();
        public AccountController(IUserManagerService userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Login(string username)
        {
            ViewBag.UserName = username;
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", "Account", new { username = loginModel.Username });
            }

            var user = await _userManager.LoginAsync(_cancellationTokenSource.Token, loginModel);

            if (user == null || user.UserId == Guid.Empty)
            {
                AlertHelper.Notify(this, "Invalid Account", "Please verify your username and password", notificationType: AlertType.error);
                return RedirectToAction("Login", "Account", new { username = loginModel.Username });
            }
            else
            {
                UpdateIdentity(user);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return LocalRedirect(returnUrl);
                }
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        public void UpdateIdentity(DTOAuthenticationResponse auth)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, auth.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, auth.Username));
            claims.Add(new Claim(ClaimTypes.Role, auth.Roles));
            claims.Add(new Claim(ClaimTypes.Sid, auth.Token));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties();
            props.IsPersistent = true;
            props.ExpiresUtc = DateTime.UtcNow.AddMinutes(16);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
        }

        [HttpGet]
        public async Task<IActionResult> ExtendSession()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var result = await _userManager.VerifyTokenAsync(_cancellationTokenSource.Token, new DTOTokenVerification { Token = accessToken, Username = username });
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var role = Convert.ToString(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value);
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
                claims.Add(new Claim(ClaimTypes.Name, username));
                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim(ClaimTypes.Sid, result.Token));


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                props.IsPersistent = true;
                props.ExpiresUtc = DateTime.UtcNow.AddMinutes(16);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

                var data = new { IsSuccess = true };
                return Json(data);
            }
            else
                return BadRequest(result);


        }
    }

}
