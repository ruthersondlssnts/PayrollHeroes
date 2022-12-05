using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TPS.Services.Interfaces;
using TPS.Services.Services;

namespace TPS.UnitTest
{
    [TestClass]
    public class TestCryptic
    {
        private ICommonService repo;
        public TestCryptic()
        {
            var services = new ServiceCollection();
            services.AddScoped(typeof(ICommonService), typeof(CommonService));

            var serviceProvider = services.BuildServiceProvider();
            repo = serviceProvider.GetService<ICommonService>();
        }

        [TestMethod]
        public void TestEncryptText()
        {
            string var = repo.EncryptString("{\"license_key\" : \"AAA-AAA-AAA\",\"expired\" : false,\"date_start\" : \"2021-01-01\",\"date_end\" : \"2021-12-30\",\"employee_count\" : 300}");
            string decs = repo.DecryptStirng(var);
        }
    }
}
