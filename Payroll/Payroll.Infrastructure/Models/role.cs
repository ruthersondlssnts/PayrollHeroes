using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class role
    {
        public role()
        {
            employee = new HashSet<employee>();
            role_permission = new HashSet<role_permission>();
        }

        public int role_id { get; set; }
        public string role_name { get; set; }
        public DateTime? date_deleted { get; set; }

        public role role_ { get; set; }
        public role Inverserole_ { get; set; }
        public ICollection<employee> employee { get; set; }
        public ICollection<role_permission> role_permission { get; set; }
    }
}
