using System;
using System.Collections.Generic;
using System.Text;

using Payroll.Core.Interface;
using Payroll.Core.Entities;
namespace Payroll.Service.Interface
{
    public interface IRefPayrollCutoff
    {

        IEnumerable<RefPayrollCutoffEntity> GetList();

        bool CreateOrUpdate(RefPayrollCutoffEntity obj);

        RefPayrollCutoffEntity GetByID(int id);
        bool Delete(int id);

    }
}
