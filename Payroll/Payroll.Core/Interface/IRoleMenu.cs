using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.Interface
{
    public interface IRoleMenu
    {
        IEnumerable<RoleMenuGridEntity> GetList();

        IEnumerable<RoleMenuEntity> GetList(int role_id);

        bool CreateOrUpdate(RoleMenuEntity obj);

        RoleMenuEntity GetByID(int id);
    }
}
