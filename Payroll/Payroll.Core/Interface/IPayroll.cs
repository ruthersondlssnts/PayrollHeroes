using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.Interface
{
    public interface IPayroll
    {
        void GeneratePayroll(int employee_id,int ref_payroll_cutoff_id);

        decimal ComputeSSS(decimal basePay, out decimal employeeContri, out decimal employerContri);

        decimal ComputePagibig(decimal basePay, out decimal employeeContri, out decimal employerContri);

        decimal ComputePhilhealth(decimal basePay, out decimal employeeContri, out decimal employerContri);

        decimal ComputeTax(decimal basePay, List<PayrollDetailsEntity> otherEarningsDeduction);

        //Maybe add PayType(Monthly or Daily Paid)
        //For Monthly Paid employees
        //Hourly rate = (Monthly Rate X 12) / total working days in a year/ total working hours per day
        //Php 71.88 = (15,000 X 12) / 313 / 8

 

        //For Daily paid employees
        //Hourly rate = (Daily rate/total working hours per day)
        //Php 57.00 = (456.00/8)

        decimal ComputeLate(decimal basePay, string payType, decimal late, decimal hoursPerDay);

        decimal ComputeAbsent(decimal basePay, string payType, decimal absent, decimal hoursPerDay);

        decimal ComputeUndertime(decimal basePay, string payType, decimal undertime, decimal hoursPerDay);

        decimal ComputeOvertime(decimal basePay, string payType, decimal hoursPerDay, int dayTypeId, decimal otHours);

        decimal ComputeNightDiff(decimal basePay, string payType, decimal hoursPerDay, int dayTypeId, decimal ndHours);

        List<PayrollOtherEarnings> GetOtherEarningsDeduction(int ref_employee_type_id,  int cutOff);

        List<PayrollDetailsEntity> GenerateEmployeePayroll(int employeeId, int payrollCutoffid);
    }
}
