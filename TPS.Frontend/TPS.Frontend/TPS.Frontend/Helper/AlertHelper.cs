using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static TPS.Frontend.Infrastructure.Enums;

namespace TPS.Frontend.Helper
{
    public static class AlertHelper
    {
        public static void Notify(Controller controller, string title, string message, AlertType notificationType = AlertType.success)
        {
            var msg = new
            {
                text = message,
                title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = GetProvider()
            };

            controller.TempData["AlertMessage"] = JsonConvert.SerializeObject(msg);
        }

        private static string GetProvider()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            return configuration["NotificationProvider"];
        }
    }
}
