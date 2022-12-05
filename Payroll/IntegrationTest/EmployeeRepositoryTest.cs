using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Core.Entities;
using Payroll.Infrastructure.Data;
using Payroll.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Test
{
    [TestClass]
    public class EmployeeRepositoryTest
    {
        EmployeeRepository Repo;
        List<EmployeeEntity> fakedata;
        [TestInitialize]
        public void Setup()
        {
            PayrollDBEntities db = new PayrollDBEntities();
            Repo = new EmployeeRepository();

            GenerateFakeData();
        }

        public void GenerateFakeData()
        {
            int employee_id = 0;
            var gender = new[] { "M", "F" };
            var marital = new[] { "SINGLE", "MARRIED", "WIDOW" };
            fakedata = new Faker<EmployeeEntity>()
                //Ensure all properties have rules. By default, StrictMode is false
                //Set a global policy by using Faker.DefaultStrictMode
                .StrictMode(false)
                //OrderId is deterministic
                .RuleFor(o => o.employee_id, f => employee_id++)

                .RuleFor(o => o.employee_serial, f => f.Random.AlphaNumeric(10))
                .RuleFor(o => o.last_name, f => f.Name.LastName())
                .RuleFor(o => o.first_name, f => f.Name.FirstName())
                .RuleFor(o => o.middle_name, f => f.Name.FirstName())
                .RuleFor(o => o.email_address, f => f.Internet.Email())
                .RuleFor(o => o.gender, f => f.PickRandom(gender))
                .RuleFor(o => o.contact_number, f => f.Phone.PhoneNumber())
                .RuleFor(o => o.sss, f => f.Random.AlphaNumeric(10))
                .RuleFor(o => o.pagibig, f => f.Random.AlphaNumeric(10))
                .RuleFor(o => o.philhealth, f => f.Random.AlphaNumeric(10))
                .RuleFor(o => o.marital_status, f => f.PickRandom(marital))
                .RuleFor(o => o.date_hire, f => f.Date.Between(DateTime.Parse("1/8/2019"), DateTime.Parse("1/30/2019")))
                .RuleFor(o => o.date_resign, f => f.Date.Between(DateTime.Parse("1/8/2019"), DateTime.Parse("1/30/2019")))
                .RuleFor(o => o.ref_shift_id, f => f.Random.Number(1, 1))
                .RuleFor(o => o.ref_department_id, f => f.Random.Number(1, 1))
                .RuleFor(o => o.fingerprint, f => f.Random.AlphaNumeric(10))
                .RuleFor(o => o.basic_pay, f => f.Random.Number(10000, 100000))
               .Generate(10);

        }

        [TestMethod]
        public void Insert()
        {
            var result = fakedata.First();
            Assert.IsNotNull(result);

            Repo.Add(result);
            //Assert.AreEqual(1, numberofrecord);
        }

        [TestMethod]
        public void Update()
        {
            var result = fakedata.First();
            Assert.IsNotNull(result);

            Repo.Update(result);
            //Assert.AreEqual(1, numberofrecord);
        }

    }
}
