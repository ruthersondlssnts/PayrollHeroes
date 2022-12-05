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
    public class RoleMenuService : IRoleMenu
    {
        RoleMenuRepository _repo;

        public RoleMenuService()
        {
            _repo = new RoleMenuRepository();
        }

        public bool CreateOrUpdate(RoleMenuEntity obj)
        {
            return _repo.CreateOrUpdate(obj);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public RoleMenuEntity GetByID(int id)
        {
            return _repo.GetByID(id);
        }

        public IEnumerable<RoleMenuGridEntity> GetList()
        {
            return _repo.GetList();
        }

        public IEnumerable<RoleMenuEntity> GetList(int role_id)
        {
            return _repo.GetList(role_id);
        }


    }
}
