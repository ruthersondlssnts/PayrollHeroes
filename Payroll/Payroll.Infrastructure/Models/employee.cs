using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class employee
    {
        public employee()
        {
            employee_balance = new HashSet<employee_balance>();
            employee_balance_transaction = new HashSet<employee_balance_transaction>();
            employee_timesheet = new HashSet<employee_timesheet>();
            ref_department_approver = new HashSet<ref_department_approver>();
            request_dtr = new HashSet<request_dtr>();
            request_leave = new HashSet<request_leave>();
            request_overtime = new HashSet<request_overtime>();
        }

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
        public DateTime? date_regular { get; set; }
        public DateTime? date_resign { get; set; }
        public int ref_employee_type_id { get; set; }
        public int ref_shift_id { get; set; }
        public int ref_pay_type_id { get; set; }
        public int role_id { get; set; }
        public int ref_department_id { get; set; }
        public string fingerprint { get; set; }
        public decimal? basic_pay { get; set; }
        public DateTime? date_deleted { get; set; }

        public employee employee_ { get; set; }
        public ref_department ref_department_ { get; set; }
        public ref_employee_type ref_employee_type_ { get; set; }
        public ref_pay_type ref_pay_type_ { get; set; }
        public ref_shift ref_shift_ { get; set; }
        public role role_ { get; set; }
        public employee Inverseemployee_ { get; set; }
        public employee_group employee_group { get; set; }
        public ICollection<employee_balance> employee_balance { get; set; }
        public ICollection<employee_balance_transaction> employee_balance_transaction { get; set; }
        public ICollection<employee_timesheet> employee_timesheet { get; set; }
        public ICollection<ref_department_approver> ref_department_approver { get; set; }
        public ICollection<request_dtr> request_dtr { get; set; }
        public ICollection<request_leave> request_leave { get; set; }
        public ICollection<request_overtime> request_overtime { get; set; }
    }
}
