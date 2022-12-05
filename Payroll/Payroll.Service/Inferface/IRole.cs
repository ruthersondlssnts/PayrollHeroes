using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Service.Interface
{
    public interface IRole
    {
        IEnumerable<RoleEntity> GetList();

        bool CreateOrUpdate(RoleEntity obj);

        RoleEntity GetByID(int id);

        IEnumerable<RolePermissionEntity> GetPermission(int id);

    }
}