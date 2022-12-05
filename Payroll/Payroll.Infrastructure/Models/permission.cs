using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class permission
    {
        public permission()
        {
            role_permission = new HashSet<role_permission>();
        }

        public int permission_id { get; set; }
        public string permission_name { get; set; }
        public string permission_code { get; set; }
        public bool is_enable { get; set; }

        public ICollection<role_permission> role_permission { get; set; }
    }
}
