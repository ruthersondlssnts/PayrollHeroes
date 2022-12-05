using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Core.Entities;
using Payroll.Infrastructure.Models;
using Payroll.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Test
{
    [TestClass]
    public class PayrollRepositoryTest
    {
        PayrollRepository Repo;

        [TestInitialize]
        public void Setup()
        {
            PayrollDBContext db = new PayrollDBContext();
            Repo = new PayrollRepository();
        }

        [TestMethod]
        public void GenerateEmployeePayroll()
        {
            var test = Repo.GenerateEmployeePayroll(2, 4);
        }

        [TestMethod]
        public void GetEmployeePayroll()
        {
            var test = Repo.GetList(2, 4);
        }
    }
}
