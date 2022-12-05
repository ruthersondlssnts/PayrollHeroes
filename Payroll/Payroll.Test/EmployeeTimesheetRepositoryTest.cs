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
using AutoMapper;

namespace Payroll.Test
{
    /// <summary>
    /// Summary description for EmployeeTimesheetRepositoryTest
    /// </summary>
    [TestClass]
    public class EmployeeTimesheetRepositoryTest
    {
        EmployeeTimesheetRepository Repo;
        RequestLeaveRepository RepoLeave;
        RequestOvertimeRepository RepoOT;
        RequestDTRRepository RepoDTR;
        IMapper _mapper;
        [TestInitialize]
        public void Setup()
        {
            PayrollDBContext db = new PayrollDBContext();
            Repo = new EmployeeTimesheetRepository();
            RepoLeave = new RequestLeaveRepository();
            RepoOT = new RequestOvertimeRepository();
            RepoDTR = new RequestDTRRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ref_shift_detail, RefShiftDetailEntity>();
            });
            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void GenerateNewTimesheet()
        {
            var data = Repo.GenerateNewTimesheet(5, DateTime.Parse("10/1/2019"), DateTime.Parse("10/15/2019"), 1);
            Assert.IsNotNull(data);
        }



        [TestMethod]
        public void UpdateActualIn()
        {
            var data = Repo.UpdateActualIn(5, DateTime.Parse("9/1/2019"), "12:00");
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void UpdateActualOut()
        {
            var data = Repo.UpdateActualOut(5, DateTime.Parse("9/1/2019"), "22:00");
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void ComputeNightDiff()
        {
            Repo.UpdateActualIn(5, DateTime.Parse("1/15/2020"), "12:00");
            Repo.UpdateActualOut(5, DateTime.Parse("1/15/2019"), "22:00");
            decimal nd8 = 0;
            var data = Repo.ComputeNightDiffHours(5, DateTime.Parse("9/8/2019"), out nd8);
            Assert.AreEqual(decimal.Parse("3.00"), data);
        }
        [TestMethod]
        public void ComputeNewND()
        {
            decimal nd8 = 0;
            var data = Repo.ND(2, DateTime.Parse("1/16/2020"), out nd8);
            Assert.AreEqual(decimal.Parse("1.00"), data);
        }

        [TestMethod]
        public void ComputeRenderedHourNormal()
        {
            Repo.UpdateActualIn(5, DateTime.Parse("9/1/2019"), "12:00");
            Repo.UpdateActualOut(5, DateTime.Parse("9/1/2019"), "22:00");

            var data = Repo.ComputeRenderHours(5, DateTime.Parse("9/1/2019"), 15);
            Assert.AreEqual(decimal.Parse("10.00"), data);
        }

        [TestMethod]
        public void ComputeRenderedHourLate30()
        {
            Repo.UpdateActualIn(5, DateTime.Parse("9/1/2019"), "12:30");
            Repo.UpdateActualOut(5, DateTime.Parse("9/1/2019"), "22:00");

            var data = Repo.ComputeRenderHours(5, DateTime.Parse("9/1/2019"), 15);
            Assert.AreEqual(decimal.Parse("9.50"), data);
        }

        [TestMethod]
        public void ComputeRenderedHourUndertime30()
        {
            Repo.UpdateActualIn(5, DateTime.Parse("9/1/2019"), "12:00");
            Repo.UpdateActualOut(5, DateTime.Parse("9/1/2019"), "21:30");

            var data = Repo.ComputeRenderHours(5, DateTime.Parse("9/1/2019"), 15);
            Assert.AreEqual(decimal.Parse("9.50"), data);
        }

        [TestMethod]
        public void ComputeUndertime30()
        {
            Repo.UpdateActualIn(5, DateTime.Parse("9/1/2019"), "12:00");
            Repo.UpdateActualOut(5, DateTime.Parse("9/1/2019"), "21:30");

            var data = Repo.ComputeUndertime(5, DateTime.Parse("9/1/2019"));
            Assert.AreEqual(decimal.Parse("0.50"), data);
        }

        [TestMethod]
        public void ComputeLate30()
        {
            Repo.UpdateActualIn(5, DateTime.Parse("9/1/2019"), "12:30");
            Repo.UpdateActualOut(5, DateTime.Parse("9/1/2019"), "22:00");
            var data = Repo.ComputeLate(5, DateTime.Parse("9/1/2019"), 15);
            Assert.AreEqual(decimal.Parse("0.50"), data);
        }

        [TestMethod]
        public void ComputeRenderedHourUndertimeLate30()
        {
            Repo.UpdateActualIn(5, DateTime.Parse("9/1/2019"), "12:30");
            Repo.UpdateActualOut(5, DateTime.Parse("9/1/2019"), "21:30");

            var data = Repo.ComputeRenderHours(5, DateTime.Parse("9/1/2019"), 15);
            Assert.AreEqual(decimal.Parse("9.00"), data);
        }

        [TestMethod]
        public void ComputeRenderedHourUndertimeLateExcess()
        {
            Repo.UpdateActualIn(5, DateTime.Parse("9/1/2019"), "13:00");
            Repo.UpdateActualOut(5, DateTime.Parse("9/1/2019"), "21:30");

            var data = Repo.ComputeRenderHours(5, DateTime.Parse("9/1/2019"), 15);
            Assert.AreEqual(decimal.Parse("8.50"), data);
        }

        [TestMethod]
        public void TimeSpanConverter()
        {
            var data = Repo.TimeSpanConverter("25:00");
            Assert.AreEqual("1.01:00:00", data.ToString());
        }

        [TestMethod]
        public void UpdateRenderHourNormal()
        {
            DateTime shiftdate = DateTime.Parse("9/1/2019");
            var renderedhour = Repo.ComputeRenderHours(5, shiftdate, 15);
            var data = Repo.UpdateRenderHour(5, shiftdate, renderedhour);
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void UpdateRenderHourDtrIn()
        {
            DateTime shiftdate = DateTime.Parse("9/2/2019");
            var renderedhour = Repo.ComputeRenderHours(5, shiftdate, 15);
            var data = Repo.UpdateRenderHour(5, shiftdate, renderedhour);
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void UpdateRenderHourDtrOut()
        {
            DateTime shiftdate = DateTime.Parse("9/3/2019");
            var renderedhour = Repo.ComputeRenderHours(5, shiftdate, 15);
            var data = Repo.UpdateRenderHour(5, shiftdate, renderedhour);
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void UpdateDTR()
        {
            DateTime shiftdate = DateTime.Parse("9/3/2019");
            //var renderedhour = Repo.ComputeRenderHours(5, shiftdate);
            var data = Repo.UpdateDTR(5, shiftdate, "12:00", "22:00", 1);
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void UpdateRenderHourDtrBoth()
        {
            DateTime shiftdate = DateTime.Parse("9/4/2019");
            var renderedhour = Repo.ComputeRenderHours(5, shiftdate, 15);
            var data = Repo.UpdateRenderHour(5, shiftdate, renderedhour);
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void OT_UpdateOt()
        {
            var data = Repo.UpdateOt(5, DateTime.Parse("9/1/2019"), "22:00", "23:00");
            Assert.IsTrue(data);
        }
        [TestMethod]
        public void OT_ComputeOT1Hr()
        {
            decimal ot8 = 0;
            //Repo.UpdateActualIn(5, DateTime.Parse("9/1/2019"), "12:00");
            //Repo.UpdateActualOut(5, DateTime.Parse("9/1/2019"), "23:00");
            //Repo.UpdateOt(5, DateTime.Parse("9/1/2019"), "22:00", "23:00");
            var data = Repo.ComputeOTHours(2, DateTime.Parse("1/16/2020"), false, out ot8);
            Assert.AreEqual(decimal.Parse("1.00"), data);
        }


        //ACTUAL
        // [TestMethod]
        //public void ACTUAL_1_InsertGeneneratedShift()
        //{
        //    var data = Repo.GenerateNewTimesheet(5, DateTime.Parse("9/1/2019"), DateTime.Parse("9/15/2019"), 1);
        //    var insert = Repo.InsertGeneratedtimesheet(data);
        //    Assert.IsTrue(insert);
        //}

        //Sept 1-15 data
        [TestMethod]
        public void ACTUAL_1_01To15()
        {
            var data = Repo.GenerateNewTimesheet(5, DateTime.Parse("9/1/2019"), DateTime.Parse("9/15/2019"), 1);
            var insert = Repo.InsertGeneratedtimesheet(data);
            Assert.IsTrue(insert);

            Repo.UpdateActualIn(5, DateTime.Parse("9/1/2019"), null);
            Repo.UpdateActualOut(5, DateTime.Parse("9/1/2019"), null);
            Repo.UpdateOt(5, DateTime.Parse("9/1/2019"), "20:00", "22:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/2/2019"), "08:59");
            Repo.UpdateActualOut(5, DateTime.Parse("9/2/2019"), "22:01");
            Repo.UpdateOt(5, DateTime.Parse("9/2/2019"), "09:00", "12:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/3/2019"), "08:59");
            Repo.UpdateActualOut(5, DateTime.Parse("9/3/2019"), "22:02");
            Repo.UpdateOt(5, DateTime.Parse("9/3/2019"), "09:00", "12:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/4/2019"), "08:51");
            Repo.UpdateActualOut(5, DateTime.Parse("9/4/2019"), "22:01");
            Repo.UpdateOt(5, DateTime.Parse("9/4/2019"), "09:00", "12:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/5/2019"), "08:58");
            Repo.UpdateActualOut(5, DateTime.Parse("9/5/2019"), "24:00");
            Repo.UpdateOt(5, DateTime.Parse("9/5/2019"), "09:00", "12:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/6/2019"), "08:58");
            Repo.UpdateActualOut(5, DateTime.Parse("9/6/2019"), "24:00");
            Repo.UpdateOt(5, DateTime.Parse("9/6/2019"), "09:00", "12:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/7/2019"), "10:35");
            Repo.UpdateActualOut(5, DateTime.Parse("9/7/2019"), "24:34");
            Repo.UpdateOt(5, DateTime.Parse("9/7/2019"), "11:00", "17:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/8/2019"), "19:37");
            Repo.UpdateActualOut(5, DateTime.Parse("9/8/2019"), "25:50");
            Repo.UpdateOt(5, DateTime.Parse("9/8/2019"), "19:37", "25:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/9/2019"), "11:00");
            Repo.UpdateActualOut(5, DateTime.Parse("9/9/2019"), "22:06");

            Repo.UpdateActualIn(5, DateTime.Parse("9/10/2019"), "12:25");
            Repo.UpdateActualOut(5, DateTime.Parse("9/10/2019"), "22:05");

            Repo.UpdateActualIn(5, DateTime.Parse("9/11/2019"), "12:10");
            Repo.UpdateActualOut(5, DateTime.Parse("9/11/2019"), "22:45");

            Repo.UpdateActualIn(5, DateTime.Parse("9/12/2019"), "12:13");
            Repo.UpdateActualOut(5, DateTime.Parse("9/12/2019"), "22:20");

            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void ACTUAL_2_UpdateTimekeeping_01To15()
        {
            decimal ot8 = 0;
            //DateTime dtstart = DateTime.Parse("9/1/2019");
            DateTime dtCutoffStart = DateTime.Parse("9/1/2019");
            DateTime dtCutoffEnd = DateTime.Parse("9/30/2019");
            for (DateTime dtstart = dtCutoffStart; dtstart <= dtCutoffEnd; dtstart = dtstart.AddDays(1))
            {
                var renderedhour = Repo.ComputeRenderHours(5, dtstart, 15);
                var late = Repo.ComputeLate(5, dtstart, 15);
                var under = Repo.ComputeUndertime(5, dtstart);
                var data = Repo.UpdateRenderHour(5, dtstart, renderedhour - (under + late));
                Assert.AreEqual(true, data);

                var otHr = Repo.ComputeOTHours(5, dtstart, false, out ot8);
                data = Repo.UpdateOt(5, dtstart, otHr);
                Assert.AreEqual(true, data);

                data = Repo.UpdateOt8(5, dtstart, ot8);
                Assert.AreEqual(true, data);


                data = Repo.UpdateUndertime(5, dtstart, under);
                Assert.AreEqual(true, data);


                data = Repo.UpdateLate(5, dtstart, late);
                Assert.AreEqual(true, data);

                decimal nd8 = 0;
                var nd = Repo.ComputeNightDiffHours(5, dtstart, out nd8);
                data = Repo.UpdateNightDiffHours(5, dtstart, nd);
                Assert.AreEqual(true, data);

                data = Repo.UpdateNightDiffHours8(5, dtstart, nd8);
                Assert.AreEqual(true, data);
            }

        }

        [TestMethod]
        public void ACTUAL_3_15TO30()
        {
            var data = Repo.GenerateNewTimesheet(5, DateTime.Parse("9/16/2019"), DateTime.Parse("9/30/2019"), 1);
            var insert = Repo.InsertGeneratedtimesheet(data);
            Assert.IsTrue(insert);

            Repo.UpdateActualIn(5, DateTime.Parse("9/16/2019"), "12:12");
            Repo.UpdateActualOut(5, DateTime.Parse("9/16/2019"), "22:09");


            Repo.UpdateActualIn(5, DateTime.Parse("9/17/2019"), "12:29");
            Repo.UpdateActualOut(5, DateTime.Parse("9/17/2019"), "22:21");

            Repo.UpdateActualIn(5, DateTime.Parse("9/18/2019"), "12:16");
            Repo.UpdateActualOut(5, DateTime.Parse("9/18/2019"), "22:03");

            Repo.UpdateActualIn(5, DateTime.Parse("9/19/2019"), "12:42");
            Repo.UpdateActualOut(5, DateTime.Parse("9/19/2019"), "22:03");


            Repo.UpdateActualIn(5, DateTime.Parse("9/20/2019"), "12:00");
            Repo.UpdateActualOut(5, DateTime.Parse("9/20/2019"), "24:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/21/2019"), null);
            Repo.UpdateActualOut(5, DateTime.Parse("9/21/2019"), null);

            Repo.UpdateActualIn(5, DateTime.Parse("9/22/2019"), null);
            Repo.UpdateActualOut(5, DateTime.Parse("9/22/2019"), null);

            Repo.UpdateActualIn(5, DateTime.Parse("9/23/2019"), "12:00");
            Repo.UpdateActualOut(5, DateTime.Parse("9/23/2019"), "22:00");


            Repo.UpdateActualIn(5, DateTime.Parse("9/24/2019"), "12:20");
            Repo.UpdateActualOut(5, DateTime.Parse("9/24/2019"), "27:00");
            Repo.UpdateOt(5, DateTime.Parse("9/24/2019"), "23:00", "27:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/25/2019"), "12:15");
            Repo.UpdateActualOut(5, DateTime.Parse("9/25/2019"), "22:09");

            Repo.UpdateActualIn(5, DateTime.Parse("9/26/2019"), "07:36");
            Repo.UpdateActualOut(5, DateTime.Parse("9/26/2019"), "25:00");
            Repo.UpdateOt(5, DateTime.Parse("9/26/2019"), "23:00", "25:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/27/2019"), "09:00");
            Repo.UpdateActualOut(5, DateTime.Parse("9/27/2019"), "22:02");
            Repo.UpdateOt(5, DateTime.Parse("9/27/2019"), "09:00", "11:00");

            Repo.UpdateActualIn(5, DateTime.Parse("9/28/2019"), null);
            Repo.UpdateActualOut(5, DateTime.Parse("9/28/2019"), null);

            Repo.UpdateActualIn(5, DateTime.Parse("9/29/2019"), null);
            Repo.UpdateActualOut(5, DateTime.Parse("9/29/2019"), null);

            Repo.UpdateActualIn(5, DateTime.Parse("9/30/2019"), "12:45");
            Repo.UpdateActualOut(5, DateTime.Parse("9/30/2019"), "22:00");
        }

        [TestMethod]
        public void ACTUAL_4_UpdateTimekeeping_15To30()
        {
            decimal ot8 = 0;
            //DateTime dtstart = DateTime.Parse("9/1/2019");
            DateTime dtCutoffStart = DateTime.Parse("9/1/2019");
            DateTime dtCutoffEnd = DateTime.Parse("9/30/2019");
            for (DateTime dtstart = dtCutoffStart; dtstart <= dtCutoffEnd; dtstart = dtstart.AddDays(1))
            {
                var renderedhour = Repo.ComputeRenderHours(5, dtstart, 15);
                var late = Repo.ComputeLate(5, dtstart, 15);
                var under = Repo.ComputeUndertime(5, dtstart);
                var data = Repo.UpdateRenderHour(5, dtstart, renderedhour - (under + late));
                Assert.AreEqual(true, data);

                var otHr = Repo.ComputeOTHours(5, dtstart, false, out ot8);
                data = Repo.UpdateOt(5, dtstart, otHr);
                Assert.AreEqual(true, data);

                data = Repo.UpdateOt8(5, dtstart, ot8);
                Assert.AreEqual(true, data);


                data = Repo.UpdateUndertime(5, dtstart, under);
                Assert.AreEqual(true, data);


                data = Repo.UpdateLate(5, dtstart, late);
                Assert.AreEqual(true, data);

                decimal nd8 = 0;
                var nd = Repo.ComputeNightDiffHours(5, dtstart, out nd8);
                data = Repo.UpdateNightDiffHours(5, dtstart, nd);
                Assert.AreEqual(true, data);

                data = Repo.UpdateNightDiffHours8(5, dtstart, nd8);
                Assert.AreEqual(true, data);
            }

        }

        //OCT 1-15 data
        [TestMethod]
        public void ACTUAL_1_OCT01To15()
        {
            var data = Repo.GenerateNewTimesheet(5, DateTime.Parse("10/1/2019"), DateTime.Parse("10/15/2019"), 1);
            var insert = Repo.InsertGeneratedtimesheet(data);
            Assert.IsTrue(insert);

            Repo.UpdateActualIn(5, DateTime.Parse("10/1/2019"), "12:57");
            Repo.UpdateActualOut(5, DateTime.Parse("10/1/2019"), "22:01");


            Repo.UpdateActualIn(5, DateTime.Parse("10/2/2019"), "12:36");
            Repo.UpdateActualOut(5, DateTime.Parse("10/2/2019"), "22:02");

            Repo.UpdateActualIn(5, DateTime.Parse("10/3/2019"), null);
            Repo.UpdateActualOut(5, DateTime.Parse("10/3/2019"), null);

            Repo.UpdateActualIn(5, DateTime.Parse("10/4/2019"), "12:26");
            Repo.UpdateActualOut(5, DateTime.Parse("10/4/2019"), "22:02");

            Repo.UpdateActualIn(5, DateTime.Parse("10/5/2019"), null);
            Repo.UpdateActualOut(5, DateTime.Parse("10/5/2019"), null);

            Repo.UpdateActualIn(5, DateTime.Parse("10/6/2019"), "16:00");
            Repo.UpdateActualOut(5, DateTime.Parse("10/6/2019"), "23:00");

            Repo.UpdateActualIn(5, DateTime.Parse("10/7/2019"), "12:31");
            Repo.UpdateActualOut(5, DateTime.Parse("10/7/2019"), "22:10");

            Repo.UpdateActualIn(5, DateTime.Parse("10/8/2019"), "12:58");
            Repo.UpdateActualOut(5, DateTime.Parse("10/8/2019"), "22:12");

            Repo.UpdateActualIn(5, DateTime.Parse("10/9/2019"), "12:28");
            Repo.UpdateActualOut(5, DateTime.Parse("10/9/2019"), "22:10");

            Repo.UpdateActualIn(5, DateTime.Parse("10/10/2019"), null);
            Repo.UpdateActualOut(5, DateTime.Parse("10/10/2019"), null);

            Repo.UpdateActualIn(5, DateTime.Parse("10/11/2019"), "11:59");
            Repo.UpdateActualOut(5, DateTime.Parse("10/11/2019"), "22:04");

            Repo.UpdateActualIn(5, DateTime.Parse("10/12/2019"), null);
            Repo.UpdateActualOut(5, DateTime.Parse("10/12/2019"), null);

            Repo.UpdateActualIn(5, DateTime.Parse("10/13/2019"), null);
            Repo.UpdateActualOut(5, DateTime.Parse("10/13/2019"), null);

            Repo.UpdateActualIn(5, DateTime.Parse("10/14/2019"), "12:11");
            Repo.UpdateActualOut(5, DateTime.Parse("10/14/2019"), "22:01");

            Repo.UpdateActualIn(5, DateTime.Parse("10/15/2019"), "12:31");
            Repo.UpdateActualOut(5, DateTime.Parse("10/15/2019"), "22:01");

            //FILE OT
            RequestOvertimeEntity objData = new RequestOvertimeEntity();
            objData.employee_id = 5;
            objData.overtime_date = DateTime.Parse("10/6/2019");
            objData.reason = "TEST";
            objData.time_in = "16:00";
            objData.time_out = "23:00";
            objData.approver_id = 5;
            objData.ref_overtime_type_id = 2;
            objData.ref_department_id = 1;
            objData.ref_status_id = 1;
            RepoOT.Add(objData);
            
            RepoOT.Approve(DateTime.Parse("10/6/2019"),5,"GOOD", 5);

            //FILE LEAVE
            RequestLeaveEntity obj = new RequestLeaveEntity();
            obj.employee_id = 5;
            obj.ref_leave_type_id = 1;
            obj.reason = "TEST";
            obj.leave_day = 1;
            obj.leave_date = DateTime.Parse("10/3/2019");
            obj.ref_status_id = 1;
            obj.approver_id = 5;
            obj.ref_department_id = 1;
            var dataLeave = RepoLeave.Add(obj);
            RepoLeave.Approve(1,"GOOD LEAVE",5);

            obj = new RequestLeaveEntity();
            obj.employee_id = 5;
            obj.ref_leave_type_id = 1;
            obj.reason = "TEST";
            obj.leave_day = 1;
            obj.leave_date = DateTime.Parse("10/10/2019");
            obj.ref_status_id = 1;
            obj.approver_id = 5;
            obj.ref_department_id = 1;
            dataLeave = RepoLeave.Add(obj);
            RepoLeave.Approve(1,  "GOOD LEAVE", 5);

            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void ACTUAL_SEPT01To15()
        {
            var data = Repo.GenerateNewTimesheet(6, DateTime.Parse("09/1/2019"), DateTime.Parse("9/10/2019"),3);
            var insert = Repo.InsertGeneratedtimesheet(data);
            Assert.IsTrue(insert);

            Repo.UpdateActualIn(6, DateTime.Parse("9/1/2019"), null);
            Repo.UpdateActualOut(6, DateTime.Parse("9/1/2019"), null);

            Repo.UpdateActualIn(6, DateTime.Parse("9/2/2019"), "20:27");
            Repo.UpdateActualOut(6, DateTime.Parse("9/2/2019"), "31:00");

            Repo.UpdateActualIn(6, DateTime.Parse("9/3/2019"), "20:34");
            Repo.UpdateActualOut(6, DateTime.Parse("9/3/2019"), "31:00");

            Repo.UpdateActualIn(6, DateTime.Parse("9/4/2019"), "20:33");
            Repo.UpdateActualOut(6, DateTime.Parse("9/4/2019"), "31:00");

            Repo.UpdateActualIn(6, DateTime.Parse("9/4/2019"), "20:33");
            Repo.UpdateActualOut(6, DateTime.Parse("9/4/2019"), "31:00");

            Repo.UpdateActualIn(6, DateTime.Parse("9/5/2019"), "20:45");
            Repo.UpdateActualOut(6, DateTime.Parse("9/5/2019"), "31:01");


            Repo.UpdateActualIn(6, DateTime.Parse("9/6/2019"), "20:48");
            Repo.UpdateActualOut(6, DateTime.Parse("9/6/2019"), "31:02");

            Repo.UpdateActualIn(6, DateTime.Parse("9/7/2019"), null);
            Repo.UpdateActualOut(6, DateTime.Parse("9/7/2019"), null);

            Repo.UpdateActualIn(6, DateTime.Parse("9/8/2019"), null);
            Repo.UpdateActualOut(6, DateTime.Parse("9/8/2019"), null);

            Repo.UpdateActualIn(6, DateTime.Parse("9/9/2019"), "20:43");
            Repo.UpdateActualOut(6, DateTime.Parse("9/9/2019"), "31:01");

            Repo.UpdateActualIn(6, DateTime.Parse("9/10/2019"), "20:30");
            Repo.UpdateActualOut(6, DateTime.Parse("9/10/2019"),"31:00");
            Assert.AreEqual(true, true);
        }
        [TestMethod]
        public void ACTUAL_5_SingleDateUpdate()
        {
            
            decimal ot8 = 0;
            DateTime dtstart = DateTime.Parse("10/06/2019");
            //DateTime dtCutoffStart = DateTime.Parse("9/1/2019");
            //DateTime dtCutoffEnd = DateTime.Parse("9/15/2019");
            //for (DateTime dtstart = dtCutoffStart; dtstart <= dtCutoffEnd; dtstart = dtstart.AddDays(1))
            //{
            //var renderedhour = Repo.ComputeRenderHours(5, dtstart, 15);
            //var late = Repo.ComputeLate(5, dtstart, 15);
            //var under = Repo.ComputeUndertime(5, dtstart);
            //var data = Repo.UpdateRenderHour(5, dtstart, renderedhour - (under + late));
            //Assert.AreEqual(true, data);

            //var otHr = Repo.ComputeOTHours(5, dtstart, false, out ot8);
            //data = Repo.UpdateOt(5, dtstart, otHr);
            //Assert.AreEqual(true, data);

            //data = Repo.UpdateOt8(5, dtstart, ot8);
            //Assert.AreEqual(true, data);


            //data = Repo.UpdateUndertime(5, dtstart, under);
            //Assert.AreEqual(true, data);


            //data = Repo.UpdateLate(5, dtstart, late);
            //Assert.AreEqual(true, data);

            decimal nd8 = 0;
            var nd = Repo.ComputeNightDiffHours(5, dtstart, out nd8);
            var data = Repo.UpdateNightDiffHours(5, dtstart, nd);
            Assert.AreEqual(true, data);

            data = Repo.UpdateNightDiffHours8(5, dtstart, nd8);
            Assert.AreEqual(true, data);
            //}

        }

        [TestMethod]
        public void GetAllRecordByEmployee()
        {
            DateTime dtCutoffStart = DateTime.Parse("9/1/2019");
            DateTime dtCutoffEnd = DateTime.Parse("9/30/2019");
            var data = Repo.GetAllEmployeeTimeSheet(5, dtCutoffStart, dtCutoffEnd);
            Assert.IsNotNull(data);
        }
        [TestMethod]
        public void GetAllRecordByEmployeeSum()
        {
            DateTime dtCutoffStart = DateTime.Parse("9/1/2019");
            DateTime dtCutoffEnd = DateTime.Parse("9/30/2019");
            var data = Repo.GetAllEmployeeTimeSheetSummarized(2, 4);
            Assert.IsNotNull(data);
        }
        [TestMethod]
        public void IsAbsent()
        {
            var data = Repo.IsAbsent(5, DateTime.Parse("09/2/2019"));
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void Holiday()
        {
            var data = Repo.UpdateHoliday(2, DateTime.Parse("01/29/2020"));
            Assert.IsTrue(data);
        }


        [TestMethod]
        public void AAAA_PROCESSTimesheet()
        {
            var data = Repo.ProcessTimesheet(2, DateTime.Parse("1/1/2020"), DateTime.Parse("1/31/2020"));
            Assert.IsTrue(data);
        }
    }
}
