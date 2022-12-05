using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Service.Interface
{
    public interface IEmployeeTimesheetService
    {
        IEnumerable<EmployeeTimesheetDTREntity> GetAllEmployeeTimeSheetDTR(int group_id, int ref_payroll_cutoff_id);
        List<EmployeeTimesheetEntity> GenerateNewTimesheet(int employeeId, DateTime dtCutoffStart, DateTime dtCutoffEnd,
            int refShiftId);
        List<EmployeeTimesheetEntity> GenerateNewTimesheet(int employeeId, int payroll_cutoff_id,
            int refShiftId);
        bool ProcessTimesheet(int employeeId, DateTime dtCutoffStart, DateTime dtCutoffEnd);
        bool ProcessTimesheet(int payrollCutoff);
        bool GenerateNewTimesheet(int payroll_cutoff_id);
       IEnumerable<EmployeeTimesheetEntity> GetAllEmployeeTimeSheet(int employeeId, DateTime dateStart, DateTime dateEnd);

        IEnumerable<EmployeeTimesheetEntity> GetUnprocessedTimeSheet(DateTime dateStart, DateTime dateEnd);
        IEnumerable<EmployeeTimesheetEntity> GetProcessedTimeSheet(DateTime dateStart, DateTime dateEnd);

        List<EmployeeTimesheetEntity> GetIncompleteTimesheet(DateTime dateStart, DateTime dateEnd);
        List<EmployeeTimesheetEntity> GetIncompleteTimesheet(int employeeId, DateTime dateStart, DateTime dateEnd);
        IEnumerable<EmployeeTimesheetDTREntity> GetAllEmployeeTimeSheet(int employeeId, int ref_payroll_cutoff_id);
        bool ProcessTimesheet(int employeeId, int payrollCutoff);
        EmployeeTimesheetEntity GetByID(int id);
        bool UpdateTimesheet(EmployeeTimesheetTemp data);
    }
}
