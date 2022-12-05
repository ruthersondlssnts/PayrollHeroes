using System;
using System.Collections.Generic;

namespace TPS.Frontend.Infrastructure
{
    public partial class RequestLeave : Document
    {
        public string EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        //public string FullName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        //1 for whole day or 0.5 halfay
        public decimal LeaveDay { get; set; }
        public decimal? PrevLeaveDay { get; set; }
        public string LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public string Remarks { get; set; }
        public bool IsHalf { get; set; }
        public bool IsFirstHalf { get; set; } = true;

        //Get the current FOR_APPROVAL from Approvers class
        public int ApprovalStatusId { get; set; }
        public string ApprovalStatusName { get; set; }
        public string ApproverUserId { get; set; }
        public string ApproverName { get; set; }
        public string ApproverRemarks { get; set; }
        public DateTime? ApproveDate { get; set; }

        public bool IsTimesheetProcessed { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public List<Approvers> Approvers { get; set; }
    }
}
