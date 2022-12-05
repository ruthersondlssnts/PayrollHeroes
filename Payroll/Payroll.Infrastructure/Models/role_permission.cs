using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class role_permission
    {
        public int role_permission_id { get; set; }
        public int role_id { get; set; }
        public int permission_id { get; set; }
        public bool is_enable { get; set; }

        public permission permission_ { get; set; }
        public role role_ { get; set; }
    }
}
