using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Service.Interface
{
    public interface IEmployee
    {
        IEnumerable<EmployeeEntity> GetList();

        bool CreateOrUpdate(EmployeeEntity obj);
        EmployeeEntity Login(string username, string password, out string access);
        EmployeeEntity GetByID(int id);
        bool Delete(int id);
    }
}