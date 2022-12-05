using System;
using System.Collections.Generic;

namespace TPS.Frontend.Infrastructure
{
    public class RequestDtr : Document
    {
        public string EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        //public string FullName { get; set; }
        public string ShiftIn { get; set; }
        public string ShiftOut { get; set; }
        public string ShiftName { get; set; }
        public string ShiftId { get; set; }
        public DateTime? ShiftDate { get; set; }
        public DateTime? ActualIn { get; set; }
        public DateTime? ActualOut { get; set; }

        public string Remarks { get; set; }

        public bool IsNightShift { get; set; }
        //Get the current FOR_APPROVAL from Approvers class
        public int ApprovalStatusId { get; set; }
        public string ApprovalStatusName { get; set; }
        public string ApproverUserId { get; set; }
        public string ApproverName { get; set; }
        public string ApproverRemarks { get; set; }
        public DateTime? ApproveDate { get; set; }


        public string GroupId { get; set; }
        public string GroupName { get; set; }

        public List<Approvers> Approvers { get; set; }
    }

}
