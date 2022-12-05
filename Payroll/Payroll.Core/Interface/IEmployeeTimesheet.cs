using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Core.Interface
{
    public interface IEmployeeTimesheet
    {
        List<EmployeeTimesheetEntity> GenerateNewTimesheet(int employeeId, DateTime dtCutoffStart, DateTime dtCutoffEnd,
            int refShiftId);
        List<EmployeeTimesheetEntity> GenerateNewTimesheet(int employeeId, int payroll_cutoff_id,
            int refShiftId);
        bool InsertGeneratedtimesheet(List<EmployeeTimesheetEntity> record);


        IEnumerable<EmployeeTimesheetDTREntity> GetAllEmployeeTimeSheetDTR(int group_id, int ref_payroll_cutoff_id);
        bool IsTimesheetExist(int employeeId, DateTime? dtShiftDate);
        

        
        //must be in format HH:MM
        TimeSpan TimeSpanConverter(string time);

        

        //COMPUTE
        decimal ComputeOTHours(int employeeId, DateTime? dtShiftDate, bool AutoOt, out decimal ot8);

        decimal ComputeNightDiffHours(int employeeId, DateTime? dtShiftDate, out decimal nd8);

        decimal ComputeUndertime(int employeeId, DateTime? dtShiftDate);
        decimal ComputeLate(int employeeId, DateTime? dtShiftDate, int graceperiod);

        TimeSpan ComputeWorkingHours(TimeSpan shiftStart, TimeSpan shiftEnd, TimeSpan actualIn, TimeSpan actualOut);

        decimal ComputeRenderHours(int employeeId, DateTime? dtShiftDate, int graceperiod);

        #region Insert
        bool UpdateActualIn(int employeeId, DateTime dtShiftDate, string tsIn);
        bool UpdateActualOut(int employeeId, DateTime dtShiftDate, string tsOut);

        bool UpdateDTRIn(int employeeId, DateTime dtShiftDate, string tsIn);
        bool UpdateDTROut(int employeeId, DateTime dtShiftDate, string tsOut);
        bool UpdateDTR(int employeeId, DateTime dtShiftDate, string tsIn, string tsOut, int shift_id);
        bool UpdateOt(int employeeId, DateTime dtShiftDate, string otIn, string otOut);
        bool UpdateLate(int employeeId, DateTime dtShiftDate, decimal late);
        #endregion



        #region Update
        bool UpdateRenderHour(int employeeId, DateTime dtShiftDate, decimal renderHour);

        bool UpdateOt(int employeeId, DateTime dtShiftDate, decimal Ot);

        bool UpdateOt8(int employeeId, DateTime dtShiftDate, decimal Ot8);

        bool UpdateNightDiffHours(int employeeId, DateTime dtShiftDate, decimal nd);

        bool UpdateNightDiffHours8(int employeeId, DateTime dtShiftDate, decimal nd8);

        bool UpdateUndertime(int employeeId, DateTime dtShiftDate, decimal countUnderTime);

        bool UpdateAbsent(int employeeId, DateTime dtShiftDate, decimal absent);

        bool ProcessTimesheet(int employeeId, DateTime dtCutoffStart, DateTime dtCutoffEnd);
        bool ProcessTimesheet(int employeeId, int payrollCutoff);
        #endregion

        //Execute when approve Leave has been checked
        bool IsApproveLeave(int employeeId, int employeeTimesheetId);

        bool IsHoliday(int employeeId, DateTime shiftDate);

        bool IsRestDay(int employeeId, DateTime shiftDate, int shiftId);
        bool IsAbsent(int employeeId, DateTime dtShiftDate);
       
        bool TagAsProcessed(int employeeTimesheetId);
        bool CheckDateAlreadyUsed(int employeeId, DateTime dtShiftDate);


        IEnumerable<EmployeeTimesheetEntity> GetAllEmployeeTimeSheet(int employeeId, DateTime dateStart, DateTime dateEnd);
        IEnumerable<EmployeeTimesheetDTREntity> GetAllEmployeeTimeSheet(int employeeId, int ref_payroll_cutoff_id);
        IEnumerable<EmployeeTimesheetEntity> GetUnprocessedTimeSheet(DateTime dateStart, DateTime dateEnd);
        IEnumerable<EmployeeTimesheetEntity> GetProcessedTimeSheet(DateTime dateStart, DateTime dateEnd);

        List<EmployeeTimesheetEntity> GetIncompleteTimesheet(DateTime dateStart, DateTime dateEnd);
        List<EmployeeTimesheetEntity> GetIncompleteTimesheet(int employeeId, DateTime dateStart, DateTime dateEnd);


        EmployeeTimesheetEntity GetByID(int id);
        bool UpdateTimesheet(EmployeeTimesheetTemp data);
    }
}
