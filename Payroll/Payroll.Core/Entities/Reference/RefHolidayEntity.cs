using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Core.Entities
{
    //TABLE NAME ref_holiday
    public class RefHolidayEntity
    {
        public long ref_holiday_id { get; set; }
        public DateTime holiday_date { get; set; }
        public string description { get; set; }
    }
}
