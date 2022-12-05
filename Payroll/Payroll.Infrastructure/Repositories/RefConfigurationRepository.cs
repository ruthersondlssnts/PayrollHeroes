using AutoMapper;
using Payroll.Core.Interface;
using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Payroll.Infrastructure.Models;

namespace Payroll.Infrastructure.Repositories
{
    public class RefConfigurationRepository : IRefConfiguration
    {
        private PayrollDBContext db = new PayrollDBContext();
        public bool AutoOT()
        {
            try
            {
               var data= db.ref_configuration.Where(a=>a.ptext == "autoot").FirstOrDefault();
                if (data.pvalue.ToLower() == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool Includeraceperiod()
        {
            try
            {
                var data = db.ref_configuration.Where(a => a.ptext == "includeraceperiod").FirstOrDefault();
                if (data.pvalue.ToLower() == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public int GetGracePeriod()
        {
            try
            {
                var data = db.ref_configuration.Where(a => a.ptext == "getgraceperiod").FirstOrDefault();
                if (data!= null)
                {
                    return int.Parse(data.pvalue);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public int GetMinimumOvertime()
        {
            try
            {
                var data = db.ref_configuration.Where(a => a.ptext == "minot").FirstOrDefault();
                if (data != null)
                {
                    return int.Parse(data.pvalue);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public void GetNighDiffHr1(out string ns_in, out string ns_out)
        {
            try
            {
                ns_in = "";
                ns_out = "";
                var NightDiffIn1 = db.ref_configuration.Where(a => a.ptext == "nightdiffIn1").FirstOrDefault();
                var NightDiffOut1 = db.ref_configuration.Where(a => a.ptext == "nightdiffOut1").FirstOrDefault();

                ns_in = NightDiffIn1.pvalue;
                ns_out = NightDiffOut1.pvalue;
            }
            catch
            {
                throw new Exception();
            }
        }

        public void GetNighDiffHr2(out string ns_in, out string ns_out)
        {
            try
            {
                ns_in = "";
                ns_out = "";
                var NightDiffIn2 = db.ref_configuration.Where(a => a.ptext == "nightdiffIn2").FirstOrDefault();
                var NightDiffOut2 = db.ref_configuration.Where(a => a.ptext == "nightdiffOut2").FirstOrDefault();

                ns_in = NightDiffIn2.pvalue;
                ns_out = NightDiffOut2.pvalue;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
