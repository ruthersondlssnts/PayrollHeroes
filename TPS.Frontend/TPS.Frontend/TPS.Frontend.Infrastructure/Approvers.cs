using System;

namespace TPS.Frontend.Infrastructure
{
    public class Approvers
    {
        public int ApprovalStatusId { get; set; }
        public string ApprovalStatusName { get; set; }
        public string ApproverUserId { get; set; }
        public string ApproverName { get; set; }
        public string ApproverRemarks { get; set; }
        public DateTime ApproveDate { get; set; }
    }
}
