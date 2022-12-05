using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TPS.Infrastructure;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;
using TPS.Services.Services;

namespace TPS.UnitTest
{
    [TestClass]
    public class TestBiometrics
    {
        private IBiometricsService repo;
        private IConfiguration _config;
        public TestBiometrics()
        {
            var services = new ServiceCollection();
            //services.AddSingleton(Configuration);
            services.AddSingleton(Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>());
            services.AddScoped(typeof(IConfigurationSettings), typeof(ConfigurationSettings));
            services.AddScoped(typeof(IBiometricsService), typeof(BiometricsService));
            services.AddScoped(typeof(IDBService<>), typeof(DBService<>));
            
            var serviceProvider = services.BuildServiceProvider();
            repo = serviceProvider.GetService<IBiometricsService>();
        }

        public IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", optional: false);
                    _config = builder.Build();
                }

                return _config;
            }
        }

        [TestMethod]
        public void TestCreateBioData()
        {
            
            var data = new ApiResponse<Infrastructure.Enums.StatusCode>();

            //03/01/2021
            data = repo.Create(new BiometricsData {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/01/2021"),
                ClockIn = "08:00",
                ClockOut = "15:46"
            }).Result;

            //03/02/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/02/2021"),
                ClockIn = "08:30",
                ClockOut = "18:36"
            }).Result;

            //03/03/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/03/2021"),
                ClockIn = "09:30",
                ClockOut = "18:50"
            }).Result;

            //03/04/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/04/2021"),
                ClockIn = "09:00",
                ClockOut = "17:00"
            }).Result;

            //03/05/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/05/2021"),
                ClockIn = "08:00",
                ClockOut = "17:00"
            }).Result;

            //03/06/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/06/2021"),
                ClockIn = "09:15",
                ClockOut = "22:30"
            }).Result;

            //03/07/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/07/2021"),
                ClockIn = "09:16",
                ClockOut = "18:56"
            }).Result;

            //03/08/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/08/2021"),
                ClockIn = "09:02",
                ClockOut = "18:00"
            }).Result;

            //03/09/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/09/2021"),
                ClockIn = "09:00",
                ClockOut = "22:30"
            }).Result;

            //03/10/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/10/2021"),
                ClockIn = "09:00",
                ClockOut = "18:00"
            }).Result;

            //03/11/2021
            //Tagged as no In/Out since leave
            //data = repo.Create(new BiometricsData
            //{
            //    ElectronicId = "111",
            //    LogDate = DateTime.Parse("03/11/2021"),
            //    ClockIn = "09:00",
            //    ClockOut = "18:00"
            //}).Result;


            //03/12/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/12/2021"),
                ClockIn = "09:00",
                ClockOut = "18:00"
            }).Result;


            //03/13/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/13/2021"),
                ClockIn = "07:00",
                ClockOut = "18:00"
            }).Result;

            //03/14/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/14/2021"),
                ClockIn = "06:00",
                ClockOut = "18:00"
            }).Result;

            //03/15/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "111",
                LogDate = DateTime.Parse("03/15/2021"),
                ClockIn = "11:30",
                ClockOut = "18:00"
            }).Result;
        }
        [TestMethod]
        public void TestCreateBioDataNightShift()
        {

            var data = new ApiResponse<Infrastructure.Enums.StatusCode>();

            //03/01/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "222",
                LogDate = DateTime.Parse("03/01/2021"),
                ClockIn = "23:00",
                ClockOut = "07:31"
            }).Result;

            //03/02/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "222",
                LogDate = DateTime.Parse("03/02/2021"),
                ClockIn = "22:00",
                ClockOut = "09:00"
            }).Result;

            //03/03/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "222",
                LogDate = DateTime.Parse("03/03/2021"),
                ClockIn = "19:00",
                ClockOut = "07:00"
            }).Result;

            //03/04/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "222",
                LogDate = DateTime.Parse("03/04/2021"),
                ClockIn = "20:00",
                ClockOut = "07:00"
            }).Result;

            //03/05/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "222",
                LogDate = DateTime.Parse("03/05/2021"),
                ClockIn = "23:00",
                ClockOut = "07:00"
            }).Result;

            //03/06/2021
            //data = repo.Create(new BiometricsData
            //{
            //    ElectronicId = "222",
            //    LogDate = DateTime.Parse("03/06/2021"),
            //    ClockIn = "20:00",
            //    ClockOut = "07:00"
            //}).Result;

            ////03/07/2021
            //data = repo.Create(new BiometricsData
            //{
            //    ElectronicId = "222",
            //    LogDate = DateTime.Parse("03/07/2021"),
            //    ClockIn = "20:00",
            //    ClockOut = "07:00"
            //}).Result;

            //03/08/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "222",
                LogDate = DateTime.Parse("03/08/2021"),
                ClockIn = "20:00",
                ClockOut = "07:00"
            }).Result;

            //03/09/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "222",
                LogDate = DateTime.Parse("03/09/2021"),
                ClockIn = "19:30",
                ClockOut = "07:30"
            }).Result;

            //03/10/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "222",
                LogDate = DateTime.Parse("03/10/2021"),
                ClockIn = "21:00",
                ClockOut = "08:30"
            }).Result;

            //03/11/2021
            //Tagged as no In/Out since leave
            //data = repo.Create(new BiometricsData
            //{
            //    ElectronicId = "111",
            //    LogDate = DateTime.Parse("03/11/2021"),
            //    ClockIn = "09:00",
            //    ClockOut = "18:00"
            //}).Result;


            //03/12/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "222",
                LogDate = DateTime.Parse("03/12/2021"),
                ClockIn = "22:30",
                ClockOut = "07:00"
            }).Result;


            //03/13/2021
            //data = repo.Create(new BiometricsData
            //{
            //    ElectronicId = "222",
            //    LogDate = DateTime.Parse("03/13/2021"),
            //    ClockIn = "20:00",
            //    ClockOut = "07:00"
            //}).Result;

            ////03/14/2021
            //data = repo.Create(new BiometricsData
            //{
            //    ElectronicId = "222",
            //    LogDate = DateTime.Parse("03/14/2021"),
            //    ClockIn = "20:00",
            //    ClockOut = "07:00"
            //}).Result;

            //03/15/2021
            data = repo.Create(new BiometricsData
            {
                ElectronicId = "222",
                LogDate = DateTime.Parse("03/15/2021"),
                ClockIn = "22:15",
                ClockOut = "07:00"
            }).Result;
        }
    }
}
