using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Core.Entities;
using Payroll.Infrastructure.Models;
using Payroll.Infrastructure.Repositories;

namespace Payroll.Test
{
    [TestClass]
    public class RequestLeaveRepositoryTest
    {
        RequestLeaveRepository Repo;
        IMapper _mapper;
        [TestInitialize]
        public void Setup()
        {
            PayrollDBContext db = new PayrollDBContext();
            Repo = new RequestLeaveRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<request_leave, RequestLeaveEntity>();
            });
            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void Create()
        {
            RequestLeaveEntity obj = new RequestLeaveEntity();
            obj.employee_id = 5;
            obj.ref_leave_type_id = 1;
            obj.reason = "TEST";
            obj.leave_day = 1;
            obj.leave_date = DateTime.Parse("10/1/2019");
            obj.ref_status_id = 1;
            obj.approver_id = 5;
            obj.ref_department_id = 1;
            var data = Repo.Add(obj);
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void Update()
        {
            RequestLeaveEntity obj = new RequestLeaveEntity();
            obj.request_leave_id = 1;
            obj.ref_department_id = 1;
            obj.ref_leave_type_id = 1;
            obj.employee_id = 5;
            obj.reason = "TEST";
            obj.leave_day = 1;
            obj.leave_date = DateTime.Parse("10/1/2019");
            obj.ref_status_id = 1;
            obj.approver_id = 5;
            var data = Repo.Update(obj);
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void GetApprovedLeave()
        {
            decimal leave = 0;
            int ref_leave_type_id = 0;
            bool data = Repo.GetApprovedLeave(5, DateTime.Parse("10/1/2019"), out leave, out ref_leave_type_id);
            Assert.IsTrue(data);
        }
    }
}
