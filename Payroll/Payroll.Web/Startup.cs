using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payroll.Infrastructure.Models;

namespace Payroll.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var ConnectionString = Configuration.GetConnectionString("PayrollDBEntities");

            //Entity Framework  
            services.AddDbContext<PayrollDBContext>(options => options.UseSqlServer(ConnectionString));
            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                   .AddCookie(options =>
                   {
                       options.Cookie.Expiration = TimeSpan.FromMinutes(16);
                       options.ExpireTimeSpan = TimeSpan.FromMinutes(16);
                       options.SlidingExpiration = true;
                       //options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                       //options.SlidingExpiration = true;
                       options.LoginPath = "/Account/Login/";
                   });

            //SESSION
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(16);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

            //services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            //Add [ValidateAntiForgeryToken]
            //Add in ajax
            //headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //VERY important for authorization tag
            app.UseAuthentication();

            //enable session before MVC
            app.UseSession();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}");

                //routes.MapRoute(
                //    "Default", // Route name
                //    "Timekeeping/{controller}/{action}/{id}", // URL with parameters
                //    new { controller = "Account", action = "Login" } // Parameter defaults
                //);

                routes.MapRoute(
                name: "default_route",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Account", action = "Login" });
            });


        }
    }
}
