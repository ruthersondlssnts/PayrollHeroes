using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Core.Entities;
using Payroll.Infrastructure.Models;
using Payroll.Infrastructure.Repositories;

namespace Payroll.Test
{
    [TestClass]
    public class RequestDtrRepositoryTest
    {
        RequestDTRRepository Repo;
        IMapper _mapper;
        [TestInitialize]
        public void Setup()
        {
            PayrollDBContext db = new PayrollDBContext();
            Repo = new RequestDTRRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<request_dtr, RequestDTREntity>();
            });
            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void Create()
        {
            RequestDTREntity obj = new RequestDTREntity();
            obj.employee_id = 5;
            obj.reason = "TEST";
            obj.ref_shift_id = 1;
            obj.shift_date = DateTime.Parse("10/1/2019");
            obj.ref_status_id = 1;
            obj.time_in = "12:00";
            obj.time_out = "22:00";
            obj.approver_id = 5;
            var data = Repo.Add(obj);
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void Update()
        {
            RequestDTREntity obj = new RequestDTREntity();
            obj.request_dtr_id = 1;
            obj.employee_id = 5;
            obj.reason = "TEST";
            obj.ref_shift_id = 1;
            obj.shift_date = DateTime.Parse("10/1/2019");
            obj.ref_status_id = 1;
            obj.time_in = "12:00";
            obj.time_out = "22:00";
            obj.approver_id = 5;
            var data = Repo.Update(obj);
            Assert.IsTrue(data);
        }
    }
}
