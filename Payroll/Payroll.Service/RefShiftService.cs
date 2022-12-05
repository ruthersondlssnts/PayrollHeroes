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
    public class RefShiftService : IRefShift
    {
        RefShiftRepository _refshiftrepo;

        public RefShiftService()
        {
            _refshiftrepo = new RefShiftRepository();
        }

        public bool CreateOrUpdate(RefShiftEntity obj)
        {
           return _refshiftrepo.CreateOrUpdate(obj);
        }

        public bool Delete(int id)
        {
            return _refshiftrepo.Delete(id);
        }

        public RefShiftEntity GetByID(int id)
        {
            return _refshiftrepo.GetByID(id);
        }

        public IEnumerable<RefShiftEntity> GetList()
        {
            return _refshiftrepo.GetList();
        }

        public RefShiftEntity GetShift(int id)
        {
            return _refshiftrepo.GetShift(id);
        }

        public IEnumerable<RefShiftDetailEntity> GetShiftDetails(int id)
        {
            return _refshiftrepo.GetShiftDetails(id);
        }

        public List<RefShiftDetailEntity> GetDaysEmpty()
        {
            return _refshiftrepo.GetDaysEmpty();
        }

        public bool Exist(string name)
        {
            return _refshiftrepo.Exist(name);
        }
    }
}
