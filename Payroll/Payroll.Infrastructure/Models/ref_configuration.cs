using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_configuration
    {
        public int config_id { get; set; }
        public string ptext { get; set; }
        public string pvalue { get; set; }
    }
}
