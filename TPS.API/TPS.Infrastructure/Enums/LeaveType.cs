using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TPS.Infrastructure.Enums
{
    public enum LeaveType
    {
        [Description("Sick Leave with Pay")]
        SLWP = 1,

        [Description("Vacation Leave with Pay")]
        VLWP = 2,

        [Description("Sick Leave without Pay")]
        SLWOP = 3,

        [Description("Vacation Leave without Pay")]
        VLWOP = 4,

        [Description("Official Business")]
        OB = 5,

        [Description("Maternity Leave with Pay")]
        MLWP = 6,

        [Description("Remote Work")]
        RW = 7,

        [Description("Emergency Leave")]
        EL = 8,

        [Description("Half Day")]
        HD = 9
    }
}
