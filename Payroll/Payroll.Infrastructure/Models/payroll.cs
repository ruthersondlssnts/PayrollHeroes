using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class payroll
    {
        public payroll()
        {
            payroll_details = new HashSet<payroll_details>();
        }

        public int payroll_id { get; set; }
        public int employee_id { get; set; }
        public int ref_payroll_cutoff_id { get; set; }
        public decimal total_earnings { get; set; }
        public decimal total_deduction { get; set; }
        public DateTime date_process { get; set; }
        public DateTime? date_deleted { get; set; }

        public payroll payroll_ { get; set; }
        public payroll Inversepayroll_ { get; set; }
        public ICollection<payroll_details> payroll_details { get; set; }
    }
}
