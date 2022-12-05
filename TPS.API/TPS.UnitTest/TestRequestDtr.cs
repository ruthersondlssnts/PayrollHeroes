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
    public class TestRequestDtr
    {
        private IRequestDtrService repo;
        private IConfiguration _config;
        public TestRequestDtr()
        {
            var services = new ServiceCollection();
            //services.AddSingleton(Configuration);
            services.AddSingleton(Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>());
            services.AddScoped(typeof(IConfigurationSettings), typeof(ConfigurationSettings));
            services.AddScoped(typeof(IRequestDtrService), typeof(RequestDtrService));
            services.AddScoped(typeof(IDBService<>), typeof(DBService<>));
            
            var serviceProvider = services.BuildServiceProvider();
            repo = serviceProvider.GetService<IRequestDtrService>();
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
        public void TestRequestDTR()
        {
           var data = repo.Create(new RequestDtr
            {
               ActualIn = DateTime.Parse("03/01/2021 07:00 AM"),
               ActualOut = DateTime.Parse("03/01/2021 07:00 PM"),
               EmployeeId = "0a807a04-dabc-4e00-8645-6ccd60fd3198",
               LastName = "Rizal",
               FirstName = "Jose",
               ApprovalStatusId = (int)ApprovalStatus.APPROVED,
               ApprovalStatusName = ApprovalStatus.APPROVED.ToString(),
               ApproveDate = DateTime.Now,
               ApproverName="Juan",
               ApproverRemarks = "GOOD",
               ApproverUserId = "0a807a04-dabc-4e00-8645-6ccd60fd3198",
               ShiftDate  = DateTime.Parse("03/01/2021 12:00 AM"),
               ShiftIn = "09:00",
               ShiftOut = "18:00",
               //ComputedHours = 9,
               Remarks = "Forgot to in/out"
           }).Result;
        }
    }
}
