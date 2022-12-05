using System;
using static TPS.Frontend.Infrastructure.Enums;

namespace TPS.Frontend.Infrastructure
{
    public class UserNotification : Document
    {
        public string EmployeeId { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public NotificationType Type { get; set; }

    }
}
