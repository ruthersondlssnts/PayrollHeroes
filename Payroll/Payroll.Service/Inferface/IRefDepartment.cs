using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Service.Interface
{
    public interface IRefDepartment
    {
        IEnumerable<RefDepartmentEntity> GetList();

        bool CreateOrUpdate(RefDepartmentEntity obj);

        RefDepartmentEntity GetByID(int id);

        IEnumerable<RefDepartmentApproverEntity> GetApproverList(int deptId);
    }
}