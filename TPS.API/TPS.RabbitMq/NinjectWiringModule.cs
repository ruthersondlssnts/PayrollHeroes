using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using TPS.Infrastructure.Models;
using TPS.RabbitMq.Services;
using TPS.RabbitMq.Utility;

namespace TPS.RabbitMq
{
    public class NinjectWiringModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMessageBroker>().To<MessageBroker>();
            Bind<IDBService<EmployeeTimesheet>>().To<DBService<EmployeeTimesheet>>();
            Bind<IDBService<Employee>>().To<DBService<Employee>>();
            Bind<IDBService<RefPayrollCutoff>>().To<DBService<RefPayrollCutoff>>();
            Bind<IDBService<BiometricsData>>().To<DBService<BiometricsData>>();
            Bind<IDBService<RequestDtr>>().To<DBService<RequestDtr>>();
            Bind<IDBService<RequestOvertime>>().To<DBService<RequestOvertime>>();
            Bind<IDBService<RequestLeave>>().To<DBService<RequestLeave>>();
            Bind<IDBService<RequestChangeShift>>().To<DBService<RequestChangeShift>>();
            Bind<IDBService<RefCalendarActivities>>().To<DBService<RefCalendarActivities>>();
            Bind<IDtrProcessorService>().To<DtrProcessorService>();

        }
    }
}
