
using Payroll.Core.Interface;
using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.Core.Interface
{
    public interface IRole
    {
        IEnumerable<RoleEntity> GetList();

        bool CreateOrUpdate(RoleEntity obj);

        RoleEntity GetByID(int id);

        IEnumerable<RolePermissionEntity> GetPermission(int id);

    }
}