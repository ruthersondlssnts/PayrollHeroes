using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TPS.API
{
        public class Program
        {
            public static void Main(string[] args)
            {
                //CreateHostBuilder(args).Build().Run();

                var isService = !(Debugger.IsAttached || args.Contains("--console"));

                if (isService)
                {
                    var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    Directory.SetCurrentDirectory(path); // need this because WebHost.CreateDefaultBuilder queries current directory to look for config files and content root folder. service start up sets this to win32's folder.
                    var host = CreateWebHostBuilder(args)
                        //Change this to determine the port when installed as service
                        //sc create FSP_Maintenance_Api binPath= "file location to exe file"
                        .UseUrls("http://+:9001")
                        .Build();
                    host.RunAsService();
                }
                else
                {
                    CreateWebHostBuilder(args)
                        //.UseKestrel()
                        //.UseContentRoot(Directory.GetCurrentDirectory())
                        .UseStartup<Startup>()
                        .Build().Run();
                }

            }

            public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });

            public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
        }
    
}
