using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_leave_type
    {
        public ref_leave_type()
        {
            employee_balance = new HashSet<employee_balance>();
            request_leave = new HashSet<request_leave>();
        }

        public int ref_leave_type_id { get; set; }
        public string ref_leave_type_name { get; set; }
        public string ref_leave_type_code { get; set; }
        public bool with_pay { get; set; }

        public ICollection<employee_balance> employee_balance { get; set; }
        public ICollection<request_leave> request_leave { get; set; }
    }
}
