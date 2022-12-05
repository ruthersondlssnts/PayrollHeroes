using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    public partial class RefPayrollDetailsType
    {
        public int ref_payroll_details_type_id { get; set; }
        public string ref_payroll_details_type_name { get; set; }
        public string ref_payroll_details_type_code { get; set; }
        public int? ref_day_type_id { get; set; }
        public bool earnings { get; set; }
        public bool taxable { get; set; }
        public bool company_contribution { get; set; }

  
    }
}
