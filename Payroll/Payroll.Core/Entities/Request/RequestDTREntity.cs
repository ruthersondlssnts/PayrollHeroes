using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Core.Entities
{
  public  class RequestDTREntity
    {
        public int request_dtr_id { get; set; }
        public int group_id { get; set; }
        public int employee_id { get; set; }
        public int ref_shift_id { get; set; }
        public DateTime shift_date { get; set; }
        public string time_in { get; set; }
        public string time_out { get; set; }
        public string reason { get; set; }
        public int ref_status_id { get; set; }
        public string approver_remark { get; set; }
        public int approver_id { get; set; }
        public DateTime? approver_date { get; set; }
        public DateTime? date_deleted { get; set; }

        public EmployeeEntity employee_ { get; set; }
        public RefShiftEntity ref_shift_ { get; set; }
        public RefStatusEntity ref_status_ { get; set; }
    }
}
