using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_department
    {
        public ref_department()
        {
            employee = new HashSet<employee>();
            ref_department_approver = new HashSet<ref_department_approver>();
            request_leave = new HashSet<request_leave>();
            request_overtime = new HashSet<request_overtime>();
        }

        public int ref_department_id { get; set; }
        public string department_name { get; set; }

        public ICollection<employee> employee { get; set; }
        public ICollection<ref_department_approver> ref_department_approver { get; set; }
        public ICollection<request_leave> request_leave { get; set; }
        public ICollection<request_overtime> request_overtime { get; set; }
    }
}
