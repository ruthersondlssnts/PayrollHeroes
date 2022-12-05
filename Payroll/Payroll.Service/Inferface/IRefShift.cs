using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Service.Interface
{
    public interface IRefShift
    {
        RefShiftEntity GetShift(int id);

        IEnumerable<RefShiftEntity> GetList();

        bool CreateOrUpdate(RefShiftEntity obj);

        RefShiftEntity GetByID(int id);

        bool Delete(int id);

        IEnumerable<RefShiftDetailEntity> GetShiftDetails(int id);

        List<RefShiftDetailEntity> GetDaysEmpty();
    }
}
