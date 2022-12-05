using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("userNotification")]
    public class UserNotification : Document
    {
        public string EmployeeId { get; set; }
        public bool IsRead { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
    }

    public enum NotificationType
    {
        DTRRequest = 1,
        DTRApproval = 2,
        OvertimeRequest = 3,
        OvertimeApproval = 4,
        LeaveRequest = 5,
        LeaveApproval = 6
    }

}
