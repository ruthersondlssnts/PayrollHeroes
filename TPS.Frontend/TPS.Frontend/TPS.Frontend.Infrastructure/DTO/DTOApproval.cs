using static TPS.Frontend.Infrastructure.Enums;

namespace TPS.Frontend.Infrastructure.DTO
{
    public class DTOApproval
    {
        public string Id { get; set; }
        public string ApproverUserId { get; set; }
        public string Remarks { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
    }
}
