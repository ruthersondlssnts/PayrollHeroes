using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    public class EmployeeEntity
    {
        public int employee_id { get; set; }
        public string employee_serial { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string email_address { get; set; }
        public string gender { get; set; }
        public string contact_number { get; set; }
        public string sss { get; set; }
        public string pagibig { get; set; }
        public string philhealth { get; set; }
        public string marital_status { get; set; }
        public DateTime? date_hire { get; set; }
        public DateTime? date_resign { get; set; }
        public DateTime? date_regular { get; set; }
        public int ref_employee_type_id { get; set; }
        public int ref_shift_id { get; set; }
        public int ref_department_id { get; set; }
        public string fingerprint { get; set; }
        public decimal? basic_pay { get; set; }
        public DateTime? date_deleted { get; set; }
        public int role_id { get; set; }
        public RefDepartmentEntity ref_department_ { get; set; }
        public RefShiftEntity ref_shift_ { get; set; }
        public ICollection<EmployeeTimesheetEntity> employee_timesheet { get; set; }
        public RoleEntity role_ { get; set; }
        //public ICollection<RequestOvertimeEntity> request_overtimeapprover_ { get; set; }
        //public ICollection<RequestOvertimeEntity> request_overtimeemployee_ { get; set; }

    }
    public class EmployeeModelEntity
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}