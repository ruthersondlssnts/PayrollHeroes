using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;
using TPS.Services.Services;

namespace TPS.UnitTest
{
    [TestClass]
    public class TestRequestOvertime
    {
        private IRequestOvertimeService repo;
        private IConfiguration _config;
        public TestRequestOvertime()
        {
            var services = new ServiceCollection();
            //services.AddSingleton(Configuration);
            services.AddSingleton(Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>());
            services.AddScoped(typeof(IConfigurationSettings), typeof(ConfigurationSettings));
            services.AddScoped(typeof(IRequestOvertimeService), typeof(RequestOvertimeService));
            services.AddScoped(typeof(IDBService<>), typeof(DBService<>));
            
            var serviceProvider = services.BuildServiceProvider();
            repo = serviceProvider.GetService<IRequestOvertimeService>();
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
        public void TestRequestOT()
        {
           var data = repo.Create(new RequestOvertime
            {
               OvertimeTimeIn = DateTime.Parse("03/09/2021 06:00 PM"),
               OvertimeTimeOut = DateTime.Parse("03/09/2021 10:00 PM"),
               OvertimeDate = DateTime.Parse("03/09/2021"),
               OvertimeTypeId = (int)OvertimeType.WITHPAY,
               OvertimeTypeName = OvertimeType.BANK.ToString(),
               EmployeeId = "0a807a04-dabc-4e00-8645-6ccd60fd3198",
               LastName = "Rizal",
               FirstName = "Jose",
               ApprovalStatusId = (int)ApprovalStatus.APPROVED,
               ApprovalStatusName = ApprovalStatus.APPROVED.ToString(),
               ApproveDate = DateTime.Now,
               ApproverName="Juan",
               ApproverRemarks = "GOOD",
               ApproverUserId = "0a807a04-dabc-4e00-8645-6ccd60fd3198",
               
               Remarks = "Forgot to in/out"
           }).Result;
        }
    }
}
