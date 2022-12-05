using Payroll.Core.Entities;
using Payroll.Infrastructure.Repositories;
using Payroll.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Payroll.Service
{
    public class RefPayrollCutoffService : IRefPayrollCutoff
    {
        RefPayrollCutoffRepository _refpayrollcutoffrepo;

        public RefPayrollCutoffService()
        {
            _refpayrollcutoffrepo = new RefPayrollCutoffRepository();
        }

        public bool CreateOrUpdate(RefPayrollCutoffEntity obj)
        {
           return _refpayrollcutoffrepo.CreateOrUpdate(obj);
        }

        public bool Delete(int id)
        {
            return _refpayrollcutoffrepo.Delete(id);
        }

        public RefPayrollCutoffEntity GetByID(int id)
        {
            return _refpayrollcutoffrepo.GetByID(id);
        }

        public IEnumerable<RefPayrollCutoffEntity> GetList()
        {
            return _refpayrollcutoffrepo.GetList();
        }


    }
}
