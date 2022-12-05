using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_group
    {
        public ref_group()
        {
            employee_group = new HashSet<employee_group>();
        }

        public int group_id { get; set; }
        public string name { get; set; }
        public DateTime? date_deleted { get; set; }
        public bool is_enable { get; set; }

        public ICollection<employee_group> employee_group { get; set; }
    }
}
