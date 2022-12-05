using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace TPS.RabbitMq.Repository
{
    public class Logger
    {
        private static string _path = ConfigurationManager.AppSettings["LogPath"];

        private static ILogger _errorLogger;
        private static ILogger _infoLogger;

        public Logger()
        {
            _infoLogger = new LoggerConfiguration().WriteTo.File($@"{_path}\Info-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7).CreateLogger();
            _errorLogger = new LoggerConfiguration().WriteTo.File($@"{_path}\Error-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7).CreateLogger();
        }

        public void WriteInfo<T>(T data)
        {
            _infoLogger.Write(LogEventLevel.Information, JsonConvert.SerializeObject(data));
        }

        public void WriteError<T>(T data, Exception exception)
        {
            var info = new
            {
                Payload = data,
                Exception = new
                {
                    ProjectName = exception.Source,
                    Message = exception.Message,
                    StackTrace = exception.StackTrace,
                    Data = exception.Data,
                    InnerException = exception.InnerException
                }
            };

            _errorLogger.Write(LogEventLevel.Error, JsonConvert.SerializeObject(info));
        }

        public void WriteError(string err)
        {
            _errorLogger.Write(LogEventLevel.Error, err);
        }
    }
}
