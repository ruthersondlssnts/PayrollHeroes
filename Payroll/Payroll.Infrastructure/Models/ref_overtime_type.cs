using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_overtime_type
    {
        public ref_overtime_type()
        {
            request_overtime = new HashSet<request_overtime>();
        }

        public int ref_overtime_type_id { get; set; }
        public string ref_overtime_type_name { get; set; }

        public ICollection<request_overtime> request_overtime { get; set; }
    }
}
