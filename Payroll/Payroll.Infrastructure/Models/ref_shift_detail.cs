using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_shift_detail
    {
        public int ref_shift_detail_id { get; set; }
        public int ref_shift_id { get; set; }
        public string day { get; set; }
        public decimal? required_hour { get; set; }
        public bool rest_day { get; set; }

        public ref_shift ref_shift_ { get; set; }
    }
}
