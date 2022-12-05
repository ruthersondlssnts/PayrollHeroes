using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_employee_type
    {
        public ref_employee_type()
        {
            employee = new HashSet<employee>();
        }

        public int ref_employee_type_id { get; set; }
        public string ref_employee_type_name { get; set; }
        public bool? date_deleted { get; set; }

        public ICollection<employee> employee { get; set; }
    }
}
