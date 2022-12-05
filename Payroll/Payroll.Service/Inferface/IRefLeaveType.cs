using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Service.Interface
{
    public interface IRefLeaveType
    {
        IEnumerable<RefLeaveTypeEntity> GetList();
        IEnumerable<RefLeaveTypeEntity> GetList(int empId);
        bool CreateOrUpdate(RefLeaveTypeEntity obj);

        RefLeaveTypeEntity GetByID(int id);

    }
}
