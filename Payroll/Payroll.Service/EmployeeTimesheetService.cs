using Payroll.Core.Entities;
using Payroll.Infrastructure.Repositories;
using Payroll.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Service
{
    public class EmployeeTimesheetService : IEmployeeTimesheetService
    {
        EmployeeTimesheetRepository _employeetimesheetrepo;
        
        public EmployeeTimesheetService()
        {
            _employeetimesheetrepo = new EmployeeTimesheetRepository();
        }
        public EmployeeTimesheetEntity GetByID(int id)
        {
            return _employeetimesheetrepo.GetByID(id);

        }
        public List<EmployeeTimesheetEntity> GenerateNewTimesheet(int employeeId, DateTime dtCutoffStart, DateTime dtCutoffEnd, int refShiftId)
        {
            return _employeetimesheetrepo.GenerateNewTimesheet(employeeId, dtCutoffStart, dtCutoffEnd, refShiftId);
        }

        public List<EmployeeTimesheetEntity> GenerateNewTimesheet(int employeeId, int payroll_cutoff_id, int refShiftId)
        {
            return _employeetimesheetrepo.GenerateNewTimesheet(employeeId, payroll_cutoff_id, refShiftId);

        }
        public IEnumerable<EmployeeTimesheetEntity> GetAllEmployeeTimeSheet(int employeeId, DateTime dateStart, DateTime dateEnd)
        {
            return _employeetimesheetrepo.GetAllEmployeeTimeSheet( employeeId,  dateStart,  dateEnd);
        }

        public IEnumerable<EmployeeTimesheetDTREntity> GetAllEmployeeTimeSheet(int employeeId, int ref_payroll_cutoff_id)
        {
            return _employeetimesheetrepo.GetAllEmployeeTimeSheet(employeeId, ref_payroll_cutoff_id);
        }

        public EmployeeTimesheetEntity GetAllEmployeeTimeSheetSummarized(int employeeId, int ref_payroll_cutoff_id)
        {
            return _employeetimesheetrepo.GetAllEmployeeTimeSheetSummarized(employeeId, ref_payroll_cutoff_id);
        }

        public List<EmployeeTimesheetEntity> GetIncompleteTimesheet(DateTime dateStart, DateTime dateEnd)
        {
            return _employeetimesheetrepo.GetIncompleteTimesheet(dateStart, dateEnd);
        }

        public List<EmployeeTimesheetEntity> GetIncompleteTimesheet(int employeeId, DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeTimesheetEntity> GetProcessedTimeSheet(DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeTimesheetEntity> GetUnprocessedTimeSheet(DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }

        public bool ProcessTimesheet(int employeeId, DateTime dtCutoffStart, DateTime dtCutoffEnd)
        {
            return _employeetimesheetrepo.ProcessTimesheet(employeeId, dtCutoffStart, dtCutoffEnd);
        }

        public bool ProcessTimesheet(int employeeId, int payrollCutoff)
        {
            return _employeetimesheetrepo.ProcessTimesheet(employeeId, payrollCutoff);
        }
        public bool ProcessTimesheet(int payrollCutoff)
        {
            return _employeetimesheetrepo.ProcessTimesheet(payrollCutoff);
        }
        public bool UpdateTimesheet(EmployeeTimesheetTemp data)
        {
            return _employeetimesheetrepo.UpdateTimesheet(data);
        }

        public bool GenerateNewTimesheet(int payroll_cutoff_id)
        {
            return _employeetimesheetrepo.GenerateNewTimesheet(payroll_cutoff_id);
        }

        public IEnumerable<EmployeeTimesheetDTREntity> GetAllEmployeeTimeSheetDTR(int group_id, int ref_payroll_cutoff_id)
        {
            return _employeetimesheetrepo.GetAllEmployeeTimeSheetDTR(group_id, ref_payroll_cutoff_id);
        }
    }
}
