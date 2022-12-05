using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Service.Interface
{
    public interface IEmployeeGroup
    {
        bool UpdateApproval(int employee_id, int type, bool value);
        //group_id
        IEnumerable<EmployeeGroupEntity> GetList(int id);

        bool CreateOrUpdate(EmployeeGroupEntity obj);

        bool Delete(int id);

        string Validate(int id);
    }
}