using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Core.Entities;
using Payroll.Infrastructure.Models;
using Payroll.Infrastructure.Repositories;

namespace Payroll.Test
{
    [TestClass]
    public class RequestOvertimeRepositoryTest
    {
        RequestOvertimeRepository Repo;
        IMapper _mapper;
        [TestInitialize]
        public void Setup()
        {
            PayrollDBContext db = new PayrollDBContext();
            Repo = new RequestOvertimeRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<request_overtime, RequestOvertimeEntity>();
            });
            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void CreateData()
        {
            RequestOvertimeEntity objData = new RequestOvertimeEntity();
            objData.employee_id = 5;
            objData.overtime_date = DateTime.Parse("9/1/2019");
            objData.reason = "TEST";
            objData.time_in = "22:00";
            objData.time_out = "24:00";
            objData.approver_id = 5;
            objData.ref_overtime_type_id = 2;
            objData.ref_department_id = 1;
            objData.ref_status_id = 1;
            try
            {
                Repo.Add(objData);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        [TestMethod]
        public void VerifyDataExistThenAdd()
        {
            RequestOvertimeEntity objData = new RequestOvertimeEntity();
            objData.employee_id = 5;
            objData.overtime_date = DateTime.Parse("9/2/2019");
            objData.reason = "TEST";
            objData.time_in = "23:00";
            objData.time_out = "24:00";
            objData.approver_id = 5;
            objData.ref_overtime_type_id = 2;
            objData.ref_department_id = 1;
            objData.ref_status_id = 1;
            try
            {
                if (!Repo.IsExist(5, objData.overtime_date))
                {
                    Repo.Add(objData);
                    Assert.IsTrue(true);
                }

            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        [TestMethod]
        public void ApproveOvertime()
        {
            var data = Repo.Approve(DateTime.Parse("9/1/2019"), 5,"GOOD", 5);
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void IsApproved()
        {
            string time_in = "";
            string time_out = "";

            var data = Repo.IsApproved(5, DateTime.Parse("9/1/2019"), out time_in, out time_out);
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void Check()
        {
            TimeSpan night_In = TimeSpanConverter("22:00");
            TimeSpan night_Out = TimeSpanConverter("30:00");
        }

        public TimeSpan TimeSpanConverter(string time)
        {
            string[] str = (time).Split(':');
            return new TimeSpan(int.Parse(str[0]), int.Parse(str[1]), 00);
        }
    }
}
