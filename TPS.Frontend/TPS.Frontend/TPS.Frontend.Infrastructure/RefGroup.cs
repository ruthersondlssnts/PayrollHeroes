using System.Collections.Generic;

namespace TPS.Frontend.Infrastructure
{
    public class RefGroup : Document
    {
        public string GroupName { get; set; }
        public string ManagerName { get; set; }

        public bool RequiredApprover { get; set; }
        public bool RequiredManager { get; set; }

        //Store guid comma separated
        public string PathToNode { get; set; }

        public List<GroupApprover> LeaveApprover { get; set; } = new List<GroupApprover>();
        public List<GroupApprover> OvertimeApprover { get; set; } = new List<GroupApprover>();
        public List<GroupApprover> DTRApprover { get; set; } = new List<GroupApprover>();
    }

    public class GroupApprover
    {
        public int ApproverOrder { get; set; }
        public string ApproverUserId { get; set; }
        public string ApproverName { get; set; }
        public bool RequiredApproval { get; set; }
        public string Type { get; set; }
        public string GroupId { get; set; }
    }
}
