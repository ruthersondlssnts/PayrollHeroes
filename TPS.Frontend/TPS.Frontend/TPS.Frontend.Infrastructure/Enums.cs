using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TPS.Frontend.Infrastructure
{
    public class Enums
    {
        /// <summary>
        /// Roles. Matches the record in database 
        /// </summary>
        public enum Roles
        {
            [Description("HR Manager")]
            HRManager,

            [Description("Admin")]
            Admin,

            [Description("Employee")]
            Employee,

            [Description("Manager")]
            Manager,

            [Description("Approver")]
            Approver
        }

        public enum AlertType
        {
            error,
            success,
            warning
        }
        public enum ApproverType
        {
            OVERTIME = 1,
            DTR = 2,
            LEAVE = 3,
        }
        public enum ApprovalStatus
        {
            FOR_APPROVAL = 1,
            APPROVED = 2,
            DISAPPROVED = 3,
            CANCELLED = 4,

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

        public enum PayType
        {
            SEMI_MONTHLY = 1,
            MONTHLY = 2,
            DAILY = 3,
            WEEKLY = 4,
        }
    }
}
