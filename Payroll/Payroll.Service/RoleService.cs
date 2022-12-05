using Payroll.Core.Entities;
using Payroll.Infrastructure.Repositories;
using Payroll.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Payroll.Service
{
    public class RoleService : IRole
    {
        RoleRepository _repo;

        public RoleService()
        {
            _repo = new RoleRepository();
        }

        public bool CreateOrUpdate(RoleEntity obj)
        {
           return _repo.CreateOrUpdate(obj);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public RoleEntity GetByID(int id)
        {
            return _repo.GetByID(id);
        }

        public IEnumerable<RoleEntity> GetList()
        {
            return _repo.GetList();
        }

        public IEnumerable<RolePermissionEntity> GetPermission(int id)
        {
            return _repo.GetPermission(id);
        }

        public IEnumerable<RolePermissionEntity> GetPermissionEmpty()
        {
            return _repo.GetPermissionEmpty();
        }
    }
}
