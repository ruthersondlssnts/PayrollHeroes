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
   public class RefGroupService :IRefGroup
    {
        RefGroupRepository _repo;

        public RefGroupService()
        {
            _repo = new RefGroupRepository();
        }

        public bool CreateOrUpdate(RefGroupEntity obj)
        {
            return _repo.CreateOrUpdate(obj);
        }

        public RefGroupEntity GetByID(int id)
        {
            return _repo.GetByID(id);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
        public IEnumerable<RefGroupEntity> GetList(int id)
        {
            return _repo.GetList(id);
        }

        public IEnumerable<RefGroupEntity> GetAllList()
        {
            return _repo.GetAllList();
        }
    }
}
