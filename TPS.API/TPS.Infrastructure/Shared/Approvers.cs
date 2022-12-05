using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Shared
{
    public class Approvers
    {
        public int ApprovalStatusId { get; set; }
        public string ApprovalStatusName { get; set; }
        public string ApproverUserId { get; set; }
        public string ApproverName { get; set; }
        public string ApproverRemarks { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ApproveDate { get; set; }
    }
}
