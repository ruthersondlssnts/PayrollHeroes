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
    public class RefCalendarActivityService : IRefCalendarActivity
    {
        RefCalendarActivityRepository _repo;

        public RefCalendarActivityService()
        {
            _repo = new RefCalendarActivityRepository();
        }

        public bool CreateOrUpdate(RefCalendarActivityEntity obj)
        {
           return _repo.CreateOrUpdate(obj);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public RefCalendarActivityEntity GetByID(int id)
        {
            return _repo.GetByID(id);
        }

        public IEnumerable<RefCalendarActivityEntity> GetList()
        {
            return _repo.GetList();
        }
        public IEnumerable<RefDayTypeEntity> GetDayType()
        {
            return _repo.GetDayType();
        }

        public bool Exist(DateTime dtDate)
        {
            return _repo.Exist(dtDate);
        }
    }
}
