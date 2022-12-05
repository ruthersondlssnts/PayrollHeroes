using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class employee_group
    {
        public int employee_id { get; set; }
        public int group_id { get; set; }
        public bool approver_all { get; set; }
        public bool ot_approver { get; set; }
        public bool ob_approver { get; set; }
        public bool leave_approver { get; set; }
        public bool dtr_approver { get; set; }

        public employee employee_ { get; set; }
        public ref_group group_ { get; set; }
    }
}
