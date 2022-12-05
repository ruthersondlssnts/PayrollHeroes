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
    public class RefDepartmentService : IRefDepartment
    {
        RefDepartmentRepository _repo;

        public RefDepartmentService()
        {
            _repo = new RefDepartmentRepository();
        }

        public bool CreateOrUpdate(RefDepartmentEntity obj)
        {
           return _repo.CreateOrUpdate(obj);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public IEnumerable<RefDepartmentApproverEntity> GetApproverList(int deptId)
        {
            return _repo.GetApproverList(deptId);
        }

        public RefDepartmentEntity GetByID(int id)
        {
            return _repo.GetByID(id);
        }

        public IEnumerable<RefDepartmentEntity> GetList()
        {
            return _repo.GetList();
        }



    }
}
