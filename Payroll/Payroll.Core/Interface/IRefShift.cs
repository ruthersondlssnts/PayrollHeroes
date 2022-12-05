using System;
using System.Collections.Generic;
using System.Text;

using Payroll.Core.Interface;
using Payroll.Core.Entities;
namespace Payroll.Core.Interface
{
    public interface IRefShift
    {
        RefShiftEntity GetShift(int id);

        IEnumerable<RefShiftEntity> GetList();

        bool CreateOrUpdate(RefShiftEntity obj);

        RefShiftEntity GetByID(int id);
        bool Delete(int id);

    }
}
