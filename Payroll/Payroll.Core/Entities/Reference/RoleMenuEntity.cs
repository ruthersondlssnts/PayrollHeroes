using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    public class RoleMenuEntity
    {
        public int role_menu_id { get; set; }
        public int role_menu_parent_id { get; set; }
        public int role_id { get; set; }
        public string display_name { get; set; }
        public string display_icon { get; set; }
        public string controller_name { get; set; }
        public string action_name { get; set; }
        public int ordering { get; set; }
        public bool is_enable { get; set; }
        public DateTime? date_deleted { get; set; }
    }

    public class RoleMenuGridEntity
    {
        public int role_menu_id { get; set; }
        public int role_menu_parent_id { get; set; }
        public int role_id { get; set; }
        public string role_name { get; set; }
        public string display_name { get; set; }
        public string display_icon { get; set; }
        public string controller_name { get; set; }
        public string action_name { get; set; }
        public int ordering { get; set; }
        public bool is_enable { get; set; }
        public DateTime? date_deleted { get; set; }
    }
}
