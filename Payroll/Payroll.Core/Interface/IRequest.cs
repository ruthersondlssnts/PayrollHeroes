using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.Interface
{
    public interface IRequest
    {
        bool IsExist(int employee_id, DateTime date);

        bool IsApproved(int employee_id, DateTime date);

    }
}
