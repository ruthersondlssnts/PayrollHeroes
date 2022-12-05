using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_pay_type
    {
        public ref_pay_type()
        {
            employee = new HashSet<employee>();
        }

        public int ref_pay_type_id { get; set; }
        public string ref_pay_type_name { get; set; }

        public ref_pay_type ref_pay_type_ { get; set; }
        public ref_pay_type Inverseref_pay_type_ { get; set; }
        public ICollection<employee> employee { get; set; }
    }
}
