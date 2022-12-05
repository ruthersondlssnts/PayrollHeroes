using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Core.Entities;
using Payroll.Core.Interface;
using Payroll.Infrastructure.Models;
using Payroll.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Test
{
    [TestClass]
    public class RefConfigurationTest
    {
        RefConfigurationRepository Repo;
        IMapper _mapper;
        [TestInitialize]
        public void Setup()
        {
            PayrollDBContext db = new PayrollDBContext();
            Repo = new RefConfigurationRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<request_dtr, RequestDTREntity>();
            });
            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void AutoOT()
        {
            var data = Repo.AutoOT();
            Assert.IsFalse(data);
        }

        [TestMethod]
        public void GetNighDiff1()
        {
            string nd_in = "";
            string nd_out = "";
            Repo.GetNighDiffHr1(out nd_in, out nd_out);
            Assert.IsNotNull(nd_in);
            Assert.IsNotNull(nd_out);
        }

        [TestMethod]
        public void GetNighDiff2()
        {
            string nd_in = "";
            string nd_out = "";
            Repo.GetNighDiffHr2(out nd_in, out nd_out);
            Assert.IsNotNull(nd_in);
            Assert.IsNotNull(nd_out);
        }
    }
}
