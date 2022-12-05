using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    public partial class PayrollDetailsEntity
    {
        public int payroll_details_id { get; set; }
        public int payroll_id { get; set; }
        public int ref_payroll_details_type_id { get; set; }
        public decimal? qty { get; set; }
        public decimal amount { get; set; }
        public PayrollEntity payroll_ { get; set; }
        public RefPayrollDetailsType ref_payroll_details_type_ { get; set; }
    }
}
