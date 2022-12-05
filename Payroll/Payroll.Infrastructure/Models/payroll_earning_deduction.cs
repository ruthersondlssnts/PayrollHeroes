using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class payroll_earning_deduction
    {
        public int payroll_earning_deduction_id { get; set; }
        public int ref_employee_type_id { get; set; }
        public int ref_payroll_details_type_id { get; set; }
        public DateTime? exact_date { get; set; }
        public int cutoff { get; set; }
        public decimal amount { get; set; }
        public bool recurring { get; set; }
        public DateTime? date_deleted { get; set; }

        public ref_payroll_details_type ref_payroll_details_type_ { get; set; }
    }
}
