using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_bir
    {
        public int ref_bir_id { get; set; }
        public int ref_pay_type_id { get; set; }
        public decimal salary_from { get; set; }
        public decimal salary_to { get; set; }
        public decimal add_tax { get; set; }
        public decimal subtract_tax_over { get; set; }
        public decimal multiplier { get; set; }
    }
}
