using System;
using System.Collections.Generic;
using System.Text;
using TPS.Infrastructure.Enums;

namespace TPS.Infrastructure.DTO
{
   public class DTOApproval
    {
        public string Id { get; set; }
        public string ApproverUserId { get; set; }
        public string Remarks { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
    }
}
