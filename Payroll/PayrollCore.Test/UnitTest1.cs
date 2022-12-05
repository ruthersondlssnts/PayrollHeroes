using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.InfrastructureCore.Models;
using Payroll.ServiceCore;
using System;

namespace PayrollCore.Test
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeTimesheetService Repo;

        [TestInitialize]
        public void Setup()
        {
            PayrollDBContext db = new PayrollDBContext();
            Repo = new EmployeeTimesheetService();


        }
        [TestMethod]
        public void Exist()
        {
            var data = Repo.GetAllEmployeeTimeSheet(6, DateTime.Parse("9/1/2019"), DateTime.Parse("9/15/2019"));
            Assert.IsNotNull(data);
        }
    }
}
