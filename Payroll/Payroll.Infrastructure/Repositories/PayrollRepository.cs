using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Core.Interface;
using Payroll.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Payroll.Infrastructure.Repositories
{
    public class PayrollRepository : IPayroll
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;

        decimal totalDayInYear = 232;
        decimal monthInYear = 12;

        public PayrollRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PayrollEntity, payroll>();
                cfg.CreateMap<payroll, PayrollEntity>();

                cfg.CreateMap<PayrollDetailsEntity, payroll_details>();
                cfg.CreateMap<payroll_details, PayrollDetailsEntity>();

                cfg.CreateMap<RefPayrollDetailsType, ref_payroll_details_type>();
                cfg.CreateMap<ref_payroll_details_type, RefPayrollDetailsType>();

            });
            _mapper = config.CreateMapper();
        }

        public decimal ComputeAbsent(decimal basePay, string payType, decimal absent, decimal hoursPerDay)
        {
            //Maybe add PayType(Monthly or Daily Paid)
            //For Monthly Paid employees
            //Hourly rate = (Monthly Rate X 12) / total working days in a year/ total working hours per day
            //Php 71.88 = (15,000 X 12) / 313 / 8



            //For Daily paid employees
            //Hourly rate = (Daily rate/total working hours per day)
            //Php 57.00 = (456.00/8)

            //Daily Rate = (Monthly Rate X 12) / Total working days in a year.
            if (absent == 0) return 0;
            if (payType == "SEMI_MONTHLY")
            {
                decimal dailyRate = (basePay * monthInYear) / totalDayInYear;
                decimal daysAbsent = absent / hoursPerDay;
                return dailyRate * daysAbsent;
            }


            return 0;
        }

        public decimal ComputeUndertime(decimal basePay, string payType, decimal undertime, decimal hoursPerDay)
        {
            //For Daily paid employees
            //Hourly rate = (Daily rate/total working hours per day)
            //Php 57.00 = (456.00/8)
            if (undertime == 0) return 0;
            if (payType == "SEMI_MONTHLY")
            {
                decimal dailyRate = (basePay * monthInYear) / totalDayInYear;
                decimal hourlyRate = dailyRate / hoursPerDay;
                return hourlyRate * undertime;
            }

            return 0;
        }

        public decimal ComputeLate(decimal basePay, string payType, decimal late, decimal hoursPerDay)
        {
            //For Daily paid employees
            //Hourly rate = (Daily rate/total working hours per day)
            //Php 57.00 = (456.00/8)
            if (late == 0) return 0;
            if (payType == "SEMI_MONTHLY")
            {
                decimal dailyRate = (basePay * monthInYear) / totalDayInYear;
                decimal hourlyRate = dailyRate / hoursPerDay;
                return hourlyRate * late;
            }

            return 0;
        }

        public decimal ComputeNightDiff(decimal basePay, string payType, decimal hoursPerDay, int dayTypeId, decimal ndHours)
        {
            if (payType == "SEMI_MONTHLY")
            {
                decimal dailyRate = (basePay * monthInYear) / totalDayInYear;
                decimal hourlyRate = dailyRate / hoursPerDay;

                var dayType = db.ref_day_type.Where(a => a.ref_day_type_id == dayTypeId).FirstOrDefault();

                if (dayType.nightdif_multiplier2 > 0)
                {
                    return hourlyRate * dayType.nightdif_multiplier1 * dayType.nightdif_multiplier2 * ndHours;
                }
                else
                {
                    return hourlyRate * dayType.nightdif_multiplier1 * ndHours;
                }

            }

            return 0;
        }

        public List<PayrollOtherEarnings> ComputerOvertime(decimal basePay, List<employee_timesheet> employee_timeshit, decimal hoursPerDay)
        {
            List<PayrollOtherEarnings> objReturn = new List<PayrollOtherEarnings>();
            decimal dailyRate = (basePay * monthInYear) / totalDayInYear;
            decimal hourlyRate = dailyRate / hoursPerDay;
            var refPayrollType = db.ref_payroll_details_type.ToList();
            var refDayType = db.ref_day_type.ToList();
            foreach (var row in refDayType)
            {
                var payrollType = refPayrollType.Where(a => a.ref_day_type_id == row.ref_day_type_id).FirstOrDefault();

                PayrollOtherEarnings objNew = new PayrollOtherEarnings();
                //GET Regular Working Day OT
                if (row.date_type_code == "RWD")
                {
                    decimal rwd_ot = employee_timeshit.Where(a => a.holiday_day_type_id == null && a.ref_day_type_id == row.ref_day_type_id).Sum(a => a.ot ?? 0);
                    decimal rwd_ot8 = employee_timeshit.Where(a => a.holiday_day_type_id == null && a.ref_day_type_id == row.ref_day_type_id).Sum(a => a.ot8 ?? 0);

                    if (rwd_ot > 0)
                    {
                        objNew = new PayrollOtherEarnings();
                        objNew.amount = hourlyRate * rwd_ot * row.ot_multiplier;
                        objNew.ref_payroll_details_type_id = payrollType.ref_payroll_details_type_id;
                        objNew.taxable = payrollType.taxable;
                        objNew.qty = rwd_ot;
                        objReturn.Add(objNew);
                    }
                    if (rwd_ot8 > 0)
                    {
                        objNew = new PayrollOtherEarnings();
                        objNew.amount = hourlyRate * rwd_ot8 * row.ot8_multiplier;
                        objNew.ref_payroll_details_type_id = payrollType.ref_payroll_details_type_id;
                        objNew.taxable = payrollType.taxable;
                        objNew.qty = rwd_ot;
                        objReturn.Add(objNew);
                    }

                    continue;
                }
                //Get Rest Day OT
                if (row.date_type_code == "RD")
                {
                    decimal rd_ot = employee_timeshit.Where(a => a.holiday_day_type_id == null && a.ref_day_type_id == row.ref_day_type_id).Sum(a => a.ot ?? 0);
                    decimal rd_ot8 = employee_timeshit.Where(a => a.holiday_day_type_id == null && a.ref_day_type_id == row.ref_day_type_id).Sum(a => a.ot8 ?? 0);

                    if (rd_ot > 0)
                    {
                        objNew = new PayrollOtherEarnings();
                        objNew.amount = hourlyRate * rd_ot * row.ot_multiplier;
                        objNew.ref_payroll_details_type_id = payrollType.ref_payroll_details_type_id;
                        objNew.taxable = payrollType.taxable;
                        objNew.qty = rd_ot;
                        objReturn.Add(objNew);
                    }
                    if (rd_ot8 > 0)
                    {
                        objNew = new PayrollOtherEarnings();
                        objNew.amount = hourlyRate * rd_ot8 * row.ot8_multiplier;
                        objNew.ref_payroll_details_type_id = payrollType.ref_payroll_details_type_id;
                        objNew.taxable = payrollType.taxable;
                        objNew.qty = rd_ot;
                        objReturn.Add(objNew);
                    }

                    continue;
                }

                //Get Holiday Pay
                decimal holiday_ot = employee_timeshit.Where(a => a.holiday_day_type_id != null && a.holiday_day_type_id == row.ref_day_type_id).Sum(a => a.ot ?? 0);
                decimal holiday_ot8 = employee_timeshit.Where(a => a.holiday_day_type_id != null && a.holiday_day_type_id == row.ref_day_type_id).Sum(a => a.ot8 ?? 0);


                if (holiday_ot > 0)
                {
                    objNew = new PayrollOtherEarnings();
                    objNew.amount = hourlyRate * holiday_ot * row.ot_multiplier;
                    objNew.ref_payroll_details_type_id = payrollType.ref_payroll_details_type_id;
                    objNew.taxable = payrollType.taxable;
                    objNew.qty = holiday_ot;
                    objReturn.Add(objNew);
                }
                if (holiday_ot8 > 0)
                {
                    objNew = new PayrollOtherEarnings();
                    objNew.amount = hourlyRate * holiday_ot8 * row.ot8_multiplier;
                    objNew.ref_payroll_details_type_id = payrollType.ref_payroll_details_type_id;
                    objNew.taxable = payrollType.taxable;
                    objNew.qty = holiday_ot8;
                    objReturn.Add(objNew);
                }
            }

            return objReturn;
        }

        public decimal ComputeOvertime(decimal basePay, string payType, decimal hoursPerDay, int dayTypeId, decimal otHours)
        {
            if (payType == "SEMI_MONTHLY")
            {

                decimal dailyRate = (basePay * monthInYear) / totalDayInYear;
                decimal hourlyRate = dailyRate / hoursPerDay;

                var dayType = db.ref_day_type.Where(a => a.ref_day_type_id == dayTypeId).FirstOrDefault();

                return hourlyRate * dayType.ot_multiplier * otHours;
            }

            return 0;
        }

        public decimal ComputePagibig(decimal basePay, out decimal employeeContri, out decimal employerContri)
        {
            employeeContri = 0;
            employerContri = 0;
            var pagibig = db.ref_pagibig.Where(a => a.salary_from <= basePay && a.salary_to >= basePay).FirstOrDefault();

            if (pagibig != null)
            {
                employeeContri = basePay * (decimal)pagibig.employee_contribution;
                employerContri = basePay * (decimal)pagibig.employer_contribution;

                employeeContri = employeeContri > 100 ? 100 : employeeContri;
                employerContri = employerContri > 100 ? 100 : employerContri;
                return 0;
            }
            else
            {
                return 0;
            }

        }

        public decimal ComputePhilhealth(decimal basePay, out decimal employeeContri, out decimal employerContri)
        {
            employeeContri = 0;
            employerContri = 0;
            var ph = db.ref_philhealth.Where(a => a.salary_from <= basePay && a.salary_to >= basePay).FirstOrDefault();

            if (ph != null)
            {
                employeeContri = (decimal)ph.employee_contribution;
                employerContri = (decimal)ph.employer_contribution;
                return 0;
            }
            else
            {
                return 0;
            }
        }

        public decimal ComputeSSS(decimal basePay, out decimal employeeContri, out decimal employerContri)
        {
            employeeContri = 0;
            employerContri = 0;
            var sss = db.ref_sss.Where(a => a.salary_from <= basePay && a.salary_to >= basePay).FirstOrDefault();

            if (sss != null)
            {
                employeeContri = (decimal)sss.employee_share;
                employerContri = ((decimal)sss.employer_share) + (decimal)sss.monthly_salary_credit;
                return 0;
            }
            else
            {
                return 0;
            }

        }

        public decimal ComputeTax(decimal basePay, List<PayrollDetailsEntity> data)
        {
            var bir = db.ref_bir.Where(a => a.salary_from <= basePay && a.salary_to >= basePay).FirstOrDefault();
            if (bir != null)
            {
                decimal totalTaxable = 0;
                foreach (var row in data)
                {
                    //Earnings and Deductions
                    var taxable = db.ref_payroll_details_type.Where(a => a.ref_payroll_details_type_id == row.ref_payroll_details_type_id).FirstOrDefault().taxable;
                    //Taxable
                    if (taxable)
                    {
                        totalTaxable += row.amount;
                    }
                }
                //var otherEarnings = otherEarningsDeduction.Where(a => a.taxable).Select(a => a.amount).Sum();
                return (((totalTaxable) - bir.subtract_tax_over) * bir.multiplier) + bir.add_tax;
            }
            else
            {
                return 0;
            }

        }

        public void GeneratePayroll(int employee_id, int ref_payroll_cutoff_id)
        {
            throw new NotImplementedException();
        }

        public List<PayrollOtherEarnings> GetOtherEarningsDeduction(int ref_employee_type_id, int cutOff)
        {
            List<PayrollOtherEarnings> returnList = new List<PayrollOtherEarnings>();

            PayrollOtherEarnings newRecord = new PayrollOtherEarnings();

            var getRecurring = db.payroll_earning_deduction.Where(a => a.ref_employee_type_id == ref_employee_type_id && a.cutoff == cutOff && a.recurring && a.date_deleted == null).ToList();

            foreach (var record in getRecurring)
            {
                PayrollOtherEarnings newRec = new PayrollOtherEarnings();
                var rec = db.ref_payroll_details_type.Where(a => a.ref_payroll_details_type_id == record.ref_payroll_details_type_id).FirstOrDefault();

                newRec.ref_payroll_details_type_id = record.ref_payroll_details_type_id;
                newRec.taxable = rec.taxable;
                newRec.amount = record.amount;
                returnList.Add(newRec);
            }


            return returnList;
        }

        public List<PayrollDetailsEntity> GenerateEmployeePayroll(int employeeId, int payrollCutoffid)
        {
            List<PayrollDetailsEntity> employeePayroll = new List<PayrollDetailsEntity>();
            var employee = db.employee.Where(a => a.employee_id == employeeId).Include(a => a.ref_pay_type_).FirstOrDefault();
            var payroll_cutoff = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == payrollCutoffid).FirstOrDefault();
            var employee_timeshit = db.employee_timesheet.Where(a => a.shift_date >= payroll_cutoff.cutoff_date_start && a.shift_date <= payroll_cutoff.cutoff_date_end && a.employee_id == employeeId).ToList();
            var employee_shift = db.ref_shift.Where(a => a.ref_shift_id == employee.ref_shift_id).FirstOrDefault();
            var employee_shift_details = db.ref_shift_detail.Where(a => a.ref_shift_id == employee_shift.ref_shift_id).ToList();
            //minus 1 hr for break
            decimal hrsPerDay = (decimal)employee_shift_details.Where(a => a.required_hour != 0).FirstOrDefault().required_hour - 1;

            //Basic Pay
            PayrollDetailsEntity basic = new PayrollDetailsEntity();
            basic.payroll_id = payrollCutoffid;
            basic.ref_payroll_details_type_id = (int)Enums.ref_payroll_details_type.BASIC_SALARY;
            basic.amount = (decimal)employee.basic_pay / 2;
            employeePayroll.Add(basic);

            //GET deducation
            decimal absentqty = (decimal)employee_timeshit.Sum(a => a.absent ?? 0.0m);
            decimal absent = ComputeAbsent((decimal)employee.basic_pay, employee.ref_pay_type_.ref_pay_type_name, absentqty, hrsPerDay);
            if (absentqty > 0)
            {
                PayrollDetailsEntity absentisim = new PayrollDetailsEntity();
                absentisim.payroll_id = payrollCutoffid;
                absentisim.ref_payroll_details_type_id = (int)Enums.ref_payroll_details_type.ABSENCE;
                absentisim.amount = absent;
                absentisim.qty = absentqty;
                employeePayroll.Add(absentisim);
            }

            //4   TARDINESS
            decimal lateunit = (decimal)employee_timeshit.Sum(a => a.late ?? 0);
            decimal undertimeeunit = (decimal)employee_timeshit.Sum(a => a.undertime ?? 0);

            decimal late = ComputeLate((decimal)employee.basic_pay, employee.ref_pay_type_.ref_pay_type_name, lateunit, hrsPerDay);
            decimal undertime = ComputeUndertime((decimal)employee.basic_pay, employee.ref_pay_type_.ref_pay_type_name, undertimeeunit, hrsPerDay);
            if (lateunit > 0 || undertimeeunit > 0)
            {
                PayrollDetailsEntity tardiness = new PayrollDetailsEntity();
                tardiness.payroll_id = payrollCutoffid;
                tardiness.ref_payroll_details_type_id = (int)Enums.ref_payroll_details_type.TARDINESS;
                tardiness.amount = late + undertime;
                tardiness.qty = lateunit + undertimeeunit;
                employeePayroll.Add(tardiness);
            }


            decimal sssEmployee = 0;
            decimal sssEmployer = 0;
            decimal pagibigEmployee = 0;
            decimal pagibigEmployer = 0;
            decimal philhealthEmployee = 0;
            decimal philhealthEmployer = 0;
            if (payroll_cutoff.government)
            {
                ComputeSSS((decimal)employee.basic_pay, out sssEmployee, out sssEmployer);
                if (sssEmployee > 0 || sssEmployer > 0)
                {
                    PayrollDetailsEntity sss = new PayrollDetailsEntity();
                    sss.payroll_id = payrollCutoffid;
                    sss.ref_payroll_details_type_id = (int)Enums.ref_payroll_details_type.SSSCONTRIBUTION;
                    sss.amount = sssEmployee;
                    sss.qty = null;
                    employeePayroll.Add(sss);

                    sss = new PayrollDetailsEntity();
                    sss.payroll_id = payrollCutoffid;
                    sss.ref_payroll_details_type_id = (int)Enums.ref_payroll_details_type.SSSCONTRIBUTIONCOM;
                    sss.amount = sssEmployer;
                    sss.qty = null;
                    employeePayroll.Add(sss);
                }

                ComputePagibig((decimal)employee.basic_pay, out pagibigEmployee, out pagibigEmployer);
                if (sssEmployee > 0 || sssEmployer > 0)
                {
                    PayrollDetailsEntity pagibig = new PayrollDetailsEntity();
                    pagibig.payroll_id = payrollCutoffid;
                    pagibig.ref_payroll_details_type_id = (int)Enums.ref_payroll_details_type.PHILHEALTHCONTRIBUTION;
                    pagibig.amount = pagibigEmployee;
                    pagibig.qty = null;
                    employeePayroll.Add(pagibig);

                    pagibig = new PayrollDetailsEntity();
                    pagibig.payroll_id = payrollCutoffid;
                    pagibig.ref_payroll_details_type_id = (int)Enums.ref_payroll_details_type.PHILHEALTHCONTRIBUTIONCOM;
                    pagibig.amount = pagibigEmployer;
                    pagibig.qty = null;
                    employeePayroll.Add(pagibig);
                }

                ComputePhilhealth((decimal)employee.basic_pay, out philhealthEmployee, out philhealthEmployer);
                if (philhealthEmployee > 0 || philhealthEmployer > 0)
                {
                    PayrollDetailsEntity philhealth = new PayrollDetailsEntity();
                    philhealth.payroll_id = payrollCutoffid;
                    philhealth.ref_payroll_details_type_id = (int)Enums.ref_payroll_details_type.PHILHEALTHCONTRIBUTION;
                    philhealth.amount = philhealthEmployee;
                    philhealth.qty = null;
                    employeePayroll.Add(philhealth);

                    philhealth = new PayrollDetailsEntity();
                    philhealth.payroll_id = payrollCutoffid;
                    philhealth.ref_payroll_details_type_id = (int)Enums.ref_payroll_details_type.PHILHEALTHCONTRIBUTIONCOM;
                    philhealth.amount = philhealthEmployer;
                    philhealth.qty = null;
                    employeePayroll.Add(philhealth);
                }
            }

            //Get Earnings
            var otherEarnings = GetOtherEarningsDeduction(employee.ref_employee_type_id, payroll_cutoff.cutoff);
            foreach (var others in otherEarnings)
            {
                PayrollDetailsEntity othersEarn = new PayrollDetailsEntity();
                othersEarn.payroll_id = payrollCutoffid;
                othersEarn.ref_payroll_details_type_id = others.ref_payroll_details_type_id;
                othersEarn.amount = others.amount;
                othersEarn.qty = others.qty;
                employeePayroll.Add(othersEarn);
            }
            
            //Overtime List
            var otList = ComputerOvertime((decimal)employee.basic_pay, employee_timeshit, hrsPerDay);
            foreach (var otrow in otList)
            {
                PayrollDetailsEntity ot = new PayrollDetailsEntity();
                ot.payroll_id = payrollCutoffid;
                ot.ref_payroll_details_type_id = otrow.ref_payroll_details_type_id;
                ot.amount = otrow.amount;
                ot.qty = otrow.qty;
                employeePayroll.Add(ot);

            }

            //Tax computation
            decimal totalTaxable = ComputeTax((decimal)employee.basic_pay, employeePayroll);
            PayrollDetailsEntity tax = new PayrollDetailsEntity();
            tax.payroll_id = payrollCutoffid;
            tax.ref_payroll_details_type_id =(int)Enums.ref_payroll_details_type.WITHHOLDING_TAX;
            tax.amount = totalTaxable;
            employeePayroll.Add(tax);

            SavePayroll(employeeId, payrollCutoffid, employeePayroll);
            return employeePayroll;
        }

        public void SavePayroll(int employeeId, int payrollCutoffid, List<PayrollDetailsEntity> data)
        {
            decimal totalDeduction = 0;
            decimal totalEarnings = 0;
            decimal totalTaxable = 0;

            //DELETE DATA FIRST
            payroll payMain = db.payroll.Where(a=>a.employee_id == employeeId && a.ref_payroll_cutoff_id == payrollCutoffid).FirstOrDefault();
            if (payMain != null)
            {
                payMain.date_deleted = DateTime.Now;
                db.SaveChanges();
            }

            payMain = new payroll();
            payMain.ref_payroll_cutoff_id = payrollCutoffid;
            payMain.employee_id = employeeId;
            payMain.date_process = DateTime.Now;
            payMain.total_deduction = 0;
            payMain.total_earnings = 0;
            db.payroll.Add(payMain);
            db.SaveChanges();

            foreach (var row in data)
            {
                payroll_details details = new payroll_details();
                details.payroll_id = payMain.payroll_id;
                details.ref_payroll_details_type_id = row.ref_payroll_details_type_id;
                details.qty = row.qty;
                details.amount = row.amount;
                db.payroll_details.Add(details);
                db.SaveChanges();
            }
            //Compute Total deductions
            //Compute Total Earnings
            //foreach (var row in data)
            //{
            //    //Earnings and Deductions
            //    var earningdeduction = db.ref_payroll_details_type.Where(a => a.ref_payroll_details_type_id == row.ref_payroll_details_type_id).FirstOrDefault();
            //    if (earningdeduction.earnings)
            //    {
            //        totalEarnings += row.amount;
            //    }
            //    else
            //    {
            //        totalDeduction += row.amount;
            //    }

            //    //Taxable
            //    if (earningdeduction.taxable)
            //    {
            //        totalTaxable += row.amount;
            //    }
            //}

        }

        public IEnumerable<PayrollDetailsEntity> GetList(int employee_id, int ref_payroll_cutoff_id)
        {
            IEnumerable<PayrollDetailsEntity> record;
            var data = db.payroll_details.Include(a=>a.payroll_).Include(a => a.ref_payroll_details_type_).Where(a => a.payroll_.employee_id  == employee_id && a.payroll_.ref_payroll_cutoff_id == ref_payroll_cutoff_id && a.payroll_.date_deleted == null).ToList();
            record = _mapper.Map<IEnumerable<payroll_details>, IEnumerable<PayrollDetailsEntity>>(data);

            return record;
        }
    }
}
