using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class payroll_config
    {
        public int payroll_config_id { get; set; }
        public string config_name { get; set; }
        public int config_value { get; set; }
    }
}
