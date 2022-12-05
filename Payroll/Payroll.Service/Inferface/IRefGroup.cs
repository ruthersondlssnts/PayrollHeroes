
using Payroll.Core.Interface;
using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.Service.Interface
{
    public interface IRefGroup
    {
        IEnumerable<RefGroupEntity> GetList(int id);

        bool CreateOrUpdate(RefGroupEntity obj);

        RefGroupEntity GetByID(int id);

    }
}