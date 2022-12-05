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
    public class TestRequestLeave
    {
        private IRequestLeaveService repo;
        private IConfiguration _config;
        public TestRequestLeave()
        {
            var services = new ServiceCollection();
            //services.AddSingleton(Configuration);
            services.AddSingleton(Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>());
            services.AddScoped(typeof(IConfigurationSettings), typeof(ConfigurationSettings));
            services.AddScoped(typeof(IRequestLeaveService), typeof(RequestLeaveService));
            services.AddScoped(typeof(IDBService<>), typeof(DBService<>));
            
            var serviceProvider = services.BuildServiceProvider();
            repo = serviceProvider.GetService<IRequestLeaveService>();
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
        public void TestRequestLeave1()
        {
           var data = repo.Create(new RequestLeave
            {
               DateStart = DateTime.Parse("03/10/2021"),
               DateEnd = DateTime.Parse("03/12/2021"),
               LeaveDay = 2.5m,
               //LeaveTypeId = (int)LeaveType.SLWP,
               LeaveTypeName = LeaveType.SLWP.ToString(),
               EmployeeId = "0a807a04-dabc-4e00-8645-6ccd60fd3198",
               LastName = "Rizal",
               FirstName = "Jose",
               ApprovalStatusId = (int)ApprovalStatus.APPROVED,
               ApprovalStatusName = ApprovalStatus.APPROVED.ToString(),
               ApproveDate = DateTime.Now,
               ApproverName="Juan",
               ApproverRemarks = "GOOD",
               ApproverUserId = "0a807a04-dabc-4e00-8645-6ccd60fd3198",
               IsTimesheetProcessed = false,
               Remarks = "Leave"
           }).Result;
        }
    }
}
