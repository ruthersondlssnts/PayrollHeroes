using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_department_approver
    {
        public int ref_department_approver_id { get; set; }
        public int ref_department_id { get; set; }
        public int employee_id { get; set; }
        public int ordering { get; set; }
        public DateTime? date_deleted { get; set; }

        public employee employee_ { get; set; }
        public ref_department ref_department_ { get; set; }
    }
}
