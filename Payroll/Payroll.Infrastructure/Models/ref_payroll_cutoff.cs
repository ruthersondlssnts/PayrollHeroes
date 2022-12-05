using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_payroll_cutoff
    {
        public int ref_payroll_cutoff_id { get; set; }
        public DateTime pay_date { get; set; }
        public DateTime cutoff_date_start { get; set; }
        public DateTime cutoff_date_end { get; set; }
        public bool is_processed { get; set; }
        public DateTime? date_deleted { get; set; }
        public bool government { get; set; }
        public int cutoff { get; set; }
    }
}
