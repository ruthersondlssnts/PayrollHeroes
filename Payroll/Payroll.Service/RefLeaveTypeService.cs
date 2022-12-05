using Payroll.Core.Entities;
using Payroll.Infrastructure.Repositories;
using Payroll.Service.Inferface;
using Payroll.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Payroll.Service
{
    public class RefLeaveTypeService : IRefLeaveType
    {
        RefLeaveTypeRepository _repo;

        public RefLeaveTypeService()
        {
            _repo = new RefLeaveTypeRepository();
        }

        public bool CreateOrUpdate(RefLeaveTypeEntity obj)
        {
           return _repo.CreateOrUpdate(obj);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public RefLeaveTypeEntity GetByID(int id)
        {
            return _repo.GetByID(id);
        }

        public IEnumerable<RefLeaveTypeEntity> GetList()
        {
            return _repo.GetList();
        }

        public IEnumerable<RefLeaveTypeEntity> GetList(int employeeId)
        {
            return _repo.GetList(employeeId);
        }

    }
}
