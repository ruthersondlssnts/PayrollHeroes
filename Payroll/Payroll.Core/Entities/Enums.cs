using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.Entities
{
    public class Enums
    {
        public enum access
        {
            [Description("view_settings_menu")]
            view_settings_menu,

            [Description("view_employee")]
            view_employee,

            [Description("view_shift")]
            view_shift,

            [Description("view_cutoff")]
            view_cutoff,

            [Description("view_department")]
            view_department,

            [Description("view_timesheet_menu")]
            view_timesheet_menu,

            [Description("view_timesheet_processing")]
            view_timesheet_processing,

            [Description("view_dtr")]
            view_dtr,

            [Description("view_request_menu")]
            view_request_menu,

            [Description("view_request_overtime")]
            view_request_overtime,

            [Description("view_request_leave")]
            view_request_leave,

            [Description("view_request_dtr")]
            view_request_dtr,

            [Description("view_dtr_process")]
            view_dtr_process,

            [Description("add_shift")]
            add_shift,

            [Description("add_cutoff")]
            add_cutoff,

            [Description("add_department")]
            add_department,

            [Description("add_employee")]
            add_employee,

            [Description("view_role")]
            view_role,

            [Description("add_role")]
            add_role,

            [Description("edit_role")]
            edit_role,

            [Description("view_approval_menu")]
            view_approval_menu,

            [Description("view_leave_type")]
            view_leave_type,

            [Description("add_leave_type")]
            add_leave_type,

            [Description("edit_leave_type")]
            edit_leave_type,

            [Description("view_approval_calendar")]
            view_approval_calendar,

            [Description("view_leave_balance")]
            view_leave_balance,

            [Description("view_holiday")]
            view_holiday,

            [Description("add_holiday")]
            add_holiday,

            [Description("edit_holiday")]
            edit_holiday,


            [Description("view_employee_balance")]
            view_employee_balance,

            [Description("add_employee_balance")]
            add_employee_balance,

            [Description("edit_employee_balance")]
            edit_employee_balance,


            [Description("view_employee_group")]
            view_employee_group,

            [Description("add_employee_group")]
            add_employee_group,

            [Description("edit_employee_group")]
            edit_employee_group,

            [Description("report_view_menu")]
            report_view_menu,

            [Description("report_dtr_all")]
            report_dtr_all,
        }

        public enum approver
        {
            /// <summary>
            /// APPROVER ENUMS
            /// </summary>
            [Description("approver_all")]
            approver_all,

            [Description("ot_approver")]
            ot_approver,

            [Description("ob_approver")]
            ob_approver,

            [Description("leave_approver")]
            leave_approver,

            [Description("dtr_approver")]
            dtr_approver,
        }

        public enum ref_payroll_details_type
        {

            BASIC_SALARY = 1,
            TRANSPORT_ALLOWANCE = 2,
            MEAL_ALLOWANCE = 3,
            TARDINESS = 4,
            REST_DAY_ADJ = 5,
            REST_DAY_OT_ADJ = 6,
            ABSENCE = 7,
            WITHHOLDING_TAX = 8,
            SSSCONTRIBUTION = 9,
            PHILHEALTHCONTRIBUTION = 10,
            HDMFCONTRIBUTION = 11,
            SSSCONTRIBUTIONCOM = 12,
            PHILHEALTHCONTRIBUTIONCOM = 13,
            HDMFCONTRIBUTIONCOM = 14,

            SH = 15,
            SHRD = 16,
            RH = 17,
            RHRD = 18,
            DH = 19,
            DHRD = 20,
            RWD = 21,
            RD = 22
        }

        public enum pay_typeref_pay_type
        {

            SEMI_MONTHLY = 1,
            MONTHLY = 2,
            DAILY = 3,
            WEEKLY = 4
        }
    }


    public static class Utilities
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null; // could also return string.Empty
        }

    }
}
