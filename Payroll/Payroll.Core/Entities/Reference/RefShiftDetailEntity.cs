using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    //TABLE NAME ref_shift_detail
    public partial class RefShiftDetailEntity
    {
        public int ref_shift_detail_id { get; set; }
        public int ref_shift_id { get; set; }
        public string day { get; set; }

        public decimal? required_hour { get; set; }
        public bool rest_day { get; set; }

        public virtual RefShiftEntity ref_shift_ { get; set; }
    }
}