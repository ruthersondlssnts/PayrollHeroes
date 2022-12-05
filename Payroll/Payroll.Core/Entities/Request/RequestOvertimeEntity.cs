using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Payroll.Core.Entities
{
    public class RequestOvertimeEntity
    {
        public long request_overtime_id { get; set; }
        public int employee_id { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime overtime_date { get; set; }

        //BANK or WITHPAY
        public int ref_overtime_type_id { get; set; }

        public string time_in { get; set; }
        public string time_out { get; set; }

        //Charge to
        //Possible to autofill based on logged user
        public int ref_department_id { get; set; }

        public string reason { get; set; }
        public int group_id { get; set; }

        //APPROVER DETAIL
        //1ForApproval
        //2Approved
        //3Disapproved
        public int ref_status_id { get; set; }

        public string approver_remark { get; set; }

        public int approver_id { get; set; }

        public DateTime? approver_date { get; set; }

        public DateTime? date_deleted { get; set; }

        public EmployeeEntity employee_ { get; set; }
        public RefDepartmentEntity ref_department_ { get; set; }
        public RefOtTypeEntity ref_overtime_type_ { get; set; }
        public RefStatusEntity ref_status_ { get; set; }

    }

    public class RequestOvertimeEntity_
    {
        public int request_overtime_id { get; set; }
        public int employee_id { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime overtime_date { get; set; }

        //BANK or WITHPAY
        public int ref_overtime_type_id { get; set; }

        public string time_in { get; set; }
        public string time_out { get; set; }

        //Charge to
        //Possible to autofill based on logged user
        public int ref_department_id { get; set; }

        public string reason { get; set; }


        //APPROVER DETAIL
        //1ForApproval
        //2Approved
        //3Disapproved
        public int ref_status_id { get; set; }

        public string approver_remark { get; set; }

        public int approver_id { get; set; }

        public DateTime? approver_date { get; set; }

        public DateTime? date_deleted { get; set; }


    }
}
