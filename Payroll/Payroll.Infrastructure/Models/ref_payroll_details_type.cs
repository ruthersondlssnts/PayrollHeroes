using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_payroll_details_type
    {
        public ref_payroll_details_type()
        {
            payroll_details = new HashSet<payroll_details>();
            payroll_earning_deduction = new HashSet<payroll_earning_deduction>();
        }

        public int ref_payroll_details_type_id { get; set; }
        public string ref_payroll_details_type_name { get; set; }
        public string ref_payroll_details_type_code { get; set; }
        public int? ref_day_type_id { get; set; }
        public bool earnings { get; set; }
        public bool taxable { get; set; }
        public bool company_contribution { get; set; }

        public ICollection<payroll_details> payroll_details { get; set; }
        public ICollection<payroll_earning_deduction> payroll_earning_deduction { get; set; }
    }
}
