using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Payroll.Core.Entities;

namespace Payroll.Core.Entities
{
   public class RequestLeaveEntity
    {
        public long request_leave_id { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime leave_date { get; set; }

        public int employee_id { get; set; }
        public int ref_leave_type_id { get; set; }

        //Possible for halfday using decimal
        public decimal leave_day { get; set; }

        //Charge to
        //Possible to autofill based on logged user
        public int ref_department_id { get; set; }

        public string reason { get; set; }


        public int ref_status_id { get; set; }

        //APPROVERS
        public int? approver_id { get; set; }
        public int group_id { get; set; }
        public DateTime? approved_date { get; set; }
        public string approver_remark { get; set; }
        public DateTime? date_deleted { get; set; }

        public RefDepartmentEntity ref_department_ { get; set; }
        public RefLeaveTypeEntity ref_leave_type_ { get; set; }
        public RefStatusEntity ref_status_ { get; set; }
        public EmployeeEntity employee_ { get; set; }
    }
}
