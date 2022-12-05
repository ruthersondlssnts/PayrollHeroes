using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    public partial class RefCalendarActivityEntity
    {
        public int ref_calendar_activity_id { get; set; }
        public DateTime work_date { get; set; }
        public int ref_day_type_id { get; set; }
        public string description { get; set; }
        public DateTime? date_deleted { get; set; }

        public RefDayTypeEntity ref_day_type_ { get; set; }
    }
}