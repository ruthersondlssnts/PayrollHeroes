using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    public partial class PayrollEntity
    {
        public PayrollEntity()
        {
            payroll_details = new HashSet<PayrollDetailsEntity>();
        }

        public int payroll_id { get; set; }
        public int employee_id { get; set; }
        public int ref_payroll_cutoff_id { get; set; }
        public decimal total_earnings { get; set; }
        public decimal total_deduction { get; set; }
        public DateTime date_process { get; set; }
        public DateTime? date_deleted { get; set; }
        public PayrollEntity payroll_ { get; set; }

        public ICollection<PayrollDetailsEntity> payroll_details { get; set; }
    }

    public partial class PayrollOtherEarnings
    {
        public int ref_payroll_details_type_id { get; set; }
        public decimal amount { get; set; }
        public decimal? qty { get; set; }
        public bool taxable { get; set; }
    }
}
