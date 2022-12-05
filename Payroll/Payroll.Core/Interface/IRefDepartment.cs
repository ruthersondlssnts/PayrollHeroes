
using Payroll.Core.Interface;
using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.Core.Interface
{
    public interface IRefDepartment
    {
        IEnumerable<RefDepartmentEntity> GetList();

        bool CreateOrUpdate(RefDepartmentEntity obj);

        RefDepartmentEntity GetByID(int id);

    }
}