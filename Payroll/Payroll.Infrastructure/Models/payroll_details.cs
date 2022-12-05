using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class payroll_details
    {
        public int payroll_details_id { get; set; }
        public int payroll_id { get; set; }
        public int ref_payroll_details_type_id { get; set; }
        public decimal? qty { get; set; }
        public decimal amount { get; set; }

        public payroll payroll_ { get; set; }
        public payroll_details payroll_details_ { get; set; }
        public ref_payroll_details_type ref_payroll_details_type_ { get; set; }
        public payroll_details Inversepayroll_details_ { get; set; }
    }
}
