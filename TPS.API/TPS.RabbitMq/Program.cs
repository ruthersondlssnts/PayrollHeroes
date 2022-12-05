using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Ninject;
using TPS.Infrastructure;
using TPS.Infrastructure.DTO;
using TPS.Services.Interfaces;
using TPS.Services.Services;
using TPS.Services.Utility;

namespace TPS.RabbitMq
{
    class Program
    {
        //private static IModel model;
        //private static EventingBasicConsumer consumer;
        //private static IDtrProcessorService repo;
        //private static IConfiguration _config;
        //private readonly IMessageBroker _messageBroker;
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.UseNinject(new NinjectWiringModule());
                x.Service<Service>(svc =>
                {
                    svc.ConstructUsingNinject();
                    svc.WhenStarted(tc => tc.Start());
                    svc.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.StartAutomaticallyDelayed();
                x.SetDescription($"rabbit.tps.{ConfigurationManager.AppSettings["Instance"]}");
                x.SetDisplayName($"rabbit.tps.{ConfigurationManager.AppSettings["Instance"]}");
                x.SetServiceName($"rabbit.tps.{ConfigurationManager.AppSettings["Instance"]}");
            });


            //How to isntall as service
            //1. got to directory
            //2. type name.exe install
        }
    }

}

