using Bogus;
using Microsoft.SqlServer.Types;
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
    public class RefGroupRepositoryTest
    {
        RefGroupRepository Repo;

        [TestInitialize]
        public void Setup()
        {
            PayrollDBContext db = new PayrollDBContext();
            Repo = new RefGroupRepository();

        }

        [TestMethod]
        public void GetData()
        {
            var result = Repo.GetByID(9);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void AddNewData()
        {
            RefGroupEntity objRec = new RefGroupEntity();
            objRec.name = "B1 INFRA Department";
            //objRec.level = SqlHierarchyId.Parse("/1/1/3/");

            var result = Repo.CreateOrUpdate(objRec);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetList()
        {
            var result = Repo.GetList(5);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetAllList()
        {
            var result = Repo.GetAllList();
            Assert.IsNotNull(result);

        }
    }
}
