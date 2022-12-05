using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class employee_role
    {
        public int employee_role_id { get; set; }
        public int employee_id { get; set; }
        public int role_id { get; set; }
    }
}
