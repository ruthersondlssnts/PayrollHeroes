using System;
using System.Collections.Generic;
using System.Text;

using Payroll.Core.Interface;
using Payroll.Core.Entities;
namespace Payroll.Core.Interface
{
    public interface IRefShiftDetail
    {
        List<RefShiftDetailEntity> GetShiftDetails(int shifId);
        bool ShifIsRestDay();
    }
}
