
using Payroll.Core.Interface;
using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.Core.Interface
{
    public interface IRefConfiguration
    {
        int GetGracePeriod();

        bool AutoOT();

        void GetNighDiffHr1(out  string ns_in,out string ns_out);

        void GetNighDiffHr2(out string ns_in, out string ns_out);
    }
}