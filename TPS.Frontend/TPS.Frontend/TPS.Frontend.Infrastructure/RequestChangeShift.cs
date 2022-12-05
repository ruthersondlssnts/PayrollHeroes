using System;
using System.Collections.Generic;

namespace TPS.Frontend.Infrastructure
{
    public class RequestChangeShift : Document
    {
        public string EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        //public string FullName { get; set; }
        public string ShiftIn { get; set; }
        public string ShiftOut { get; set; }

        public DateTime ShiftDate { get; set; }

        public decimal RequiredHour { get; set; }

        public string Remarks { get; set; }

        public decimal GracePeriod { get; set; }
        public bool IncludeGracePeriod { get; set; }
        public bool IsAutoOvertime { get; set; }
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
