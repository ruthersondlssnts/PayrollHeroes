using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.DTO;
using TPS.Services.Interfaces;
using TPS.Services.Services;
using TPS.Services.Utility;

namespace TPS.UnitTest
{
    [TestClass]
    public class TestEmployeeTimesheet2
    {
        private IEmployeeTimesheetService repo;
        private IConfiguration _config;
        private ConfigurationSettings _configSettings;
        public TestEmployeeTimesheet2()
        {
            var services = new ServiceCollection();
            services.AddSingleton(Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>());
            services.AddScoped(typeof(IConfigurationSettings), typeof(ConfigurationSettings));
            services.AddScoped(typeof(IEmployeeTimesheetService), typeof(EmployeeTimesheetService));
            services.AddScoped(typeof(IMessageBroker), typeof(MessageBroker));
            services.AddScoped(typeof(IDBService<>), typeof(DBService<>));
            
            var serviceProvider = services.BuildServiceProvider();
            repo = serviceProvider.GetService<IEmployeeTimesheetService>();
        }

        public IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", optional: false);
                    _config = builder.Build();
                }

                return _config;
            }
        }

        [TestMethod]
        public void ProcessTimesheet()
        {
            string EmployeeId = "c3e231dd-f05c-4267-9eb6-9ceaddbb8dfb";
            string CutoffId = "059d07e7-214c-4885-87d0-c0a6a0c7cbfe";

            //1. Generate Timesheet
            var data = repo.GenerateTimesheet(CutoffId);
        }

        [TestMethod]
        public void ProcessTimesheetOthers()
        {
            string EmployeeId = "c3e231dd-f05c-4267-9eb6-9ceaddbb8dfb";
            string CutoffId = "059d07e7-214c-4885-87d0-c0a6a0c7cbfe";

            //1. Generate Timesheet
            var data = repo.GenerateTimesheetOthers(CutoffId);
        }

        [TestMethod]
        public void ProcessTimesheetdetails()
        {
            string EmployeeId = "845e1787-afbd-4322-9efa-d6f48cc711c0";
            string CutoffId = "059d07e7-214c-4885-87d0-c0a6a0c7cbfe";

            
        }

        [TestMethod]
        public void ProcessSingleUser()
        {
            string EmployeeId = "ac1cbe51-c4c9-4734-95e7-c8c7649d5c13";
            string CutoffId = "059d07e7-214c-4885-87d0-c0a6a0c7cbfe";

            //var data = repo.GetActualInOut(EmployeeId, CutoffId);
        }

        [TestMethod]
        public void ProcessAllEmployee()
        {
            string CutoffId = "059d07e7-214c-4885-87d0-c0a6a0c7cbfe";

            //1. Generate Timesheet
            var data = repo.GenerateTimesheet(CutoffId);
        }

    }
}
