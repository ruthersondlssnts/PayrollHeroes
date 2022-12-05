using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Core.Entities
{
    public partial class EmployeeTimesheetEntity
    {
        public long employee_timesheet_id { get; set; }
        public int employee_id { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> shift_date { get; set; }
        public int ref_shift_id { get; set; }
        public int ref_day_type_id { get; set; }
        public int? holiday_day_type_id { get; set; }
        public Nullable<decimal> required_hour { get; set; }
        public Nullable<decimal> rendered_hour { get; set; }
        public string time_in1 { get; set; }
        public string time_out1 { get; set; }
        public string time_in2 { get; set; }
        public string time_out2 { get; set; }
        public string ot_in { get; set; }
        public string ot_out { get; set; }
        public Nullable<decimal> ot { get; set; }
        public Nullable<decimal> ot8 { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,###,###.00}")]
        public Nullable<decimal> night_dif { get; set; }
        public Nullable<decimal> night_dif8 { get; set; }
        public Nullable<decimal> absent { get; set; }
        public Nullable<decimal> late { get; set; }
        public Nullable<decimal> undertime { get; set; }
        public Nullable<decimal> approve_leave { get; set; }
        public Nullable<int> ref_leave_type_id { get; set; }
        public bool payroll_process { get; set; }

        public EmployeeEntity employee_ { get; set; }
        public RefDayTypeEntity ref_day_type_ { get; set; }
        public RefShiftEntity ref_shift_ { get; set; }


    }

    public partial class EmployeeTimesheetDTREntity
    {
        public long employee_timesheet_id { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> shift_date { get; set; }
        public string shift_name { get; set; }
        public string date_type_code { get; set; }
        public Nullable<decimal> required_hour { get; set; }
        public Nullable<decimal> rendered_hour { get; set; }
        public string time_in1 { get; set; }
        public string time_out1 { get; set; }
        public string ot_in { get; set; }
        public string ot_out { get; set; }
        public Nullable<decimal> late { get; set; }
        public Nullable<decimal> undertime { get; set; }
        public Nullable<decimal> ot { get; set; }
        public Nullable<decimal> ot8 { get; set; }
        public Nullable<decimal> night_dif { get; set; }
        public Nullable<decimal> absent { get; set; }
    }
    public partial class EmployeeTimesheetTemp
    {
        public string empid { get; set; }
        public string timein { get; set; }
        public string timeout { get; set; }
    }

    public partial class EmployeeTimesheetTempDevice
    {
        public string empid { get; set; }
        public DateTime shiftDate { get; set; }
        public string timein { get; set; }
        public string timeout { get; set; }
    }
}