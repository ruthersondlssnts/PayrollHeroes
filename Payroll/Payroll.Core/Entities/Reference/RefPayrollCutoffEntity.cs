using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Core.Entities
{
    //TABLE NAME ref_payroll_cutoff
    public class RefPayrollCutoffEntity
    {
        public long ref_payroll_cutoff_id { get; set; }

        public DateTime cutoff_date_start { get; set; }

        public DateTime cutoff_date_end { get; set; }

        public bool is_processed { get; set; }

        public DateTime? date_deleted { get; set; }

        public bool government { get; set; }
    }
    public class RefPayrollCutoffFormEntity
    {
        public long ref_payroll_cutoff_id { get; set; }

        public DateTime cutoff_date_start { get; set; }

        public DateTime cutoff_date_end { get; set; }



        public bool government { get; set; }
    }
}
