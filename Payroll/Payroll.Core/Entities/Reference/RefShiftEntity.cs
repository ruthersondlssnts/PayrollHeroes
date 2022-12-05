using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    //TABLE NAME ref_shift
    public partial class RefShiftEntity
    {
        public int ref_shift_id { get; set; }
        public string shift_name { get; set; }
        public string shift_in { get; set; }
        public string shift_out { get; set; }
        public string break_in { get; set; }
        public string break_out { get; set; }
        public decimal? break_hour { get; set; }
        public decimal? required_hour { get; set; }
        public string nd_start { get; set; }
        public string nd_end { get; set; }
        public decimal grace_period { get; set; }
        public bool include_grace_period { get; set; }
        public bool? is_auto_ot { get; set; }
        public bool is_nd { get; set; }
        public DateTime? date_deleted { get; set; }

        public object ref_shift_detail_ { get; set; }
        public IEnumerable< RefShiftDetailEntity> ref_shift_detail { get; set; }
        //public virtual ICollection<EmployeeEntity> employee { get; set; }
        //public virtual ICollection<EmployeeTimesheetEntity> employee_timesheet { get; set; }
        //public virtual ICollection<RefShiftDetailEntity> ref_shift_detail { get; set; }
    }
}