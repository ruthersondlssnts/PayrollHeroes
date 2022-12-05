using System;
using System.Collections.Generic;
using System.Text;
using TPS.RabbitMq.Utility;
using TPS.RabbitMq.Services;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using TPS.Infrastructure.DTO;
using TPS.RabbitMq.Repository;

namespace TPS.RabbitMq
{
    public class Service
    {
        private readonly IMessageBroker _messageBroker;
        private IDtrProcessorService repo;
        private readonly Logger _logger;
        public Service(IMessageBroker messageBroker, IDtrProcessorService _repo)
        {
            _messageBroker = messageBroker;
            repo = _repo;
            _logger = new Logger();
        }

        public void Start()
        {
            _messageBroker.Connect();
            _messageBroker.MessageReceived += Consumer_Received;
        }

        public void Stop()
        {
            _messageBroker.Dispose();
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var message = Encoding.UTF8.GetString(@event.Body.Span);
            
            var messageType = @event.BasicProperties.Type;
            try
            {
                switch (messageType)
                {
                    //On time Generation
                    case "GenerateTimesheet":
                        var GenerateTimesheet = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.GenerateTimesheet(GenerateTimesheet);
                        break;
                    case "ComputeApprovedOvertimeHr":
                        var ComputeApprovedOvertimeHr = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.ComputeApprovedOvertimeHr(ComputeApprovedOvertimeHr);
                        break;
                    case "ComputeNightShiftHours":
                        var ComputeNightShiftHours = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.ComputeNightShiftHours(ComputeNightShiftHours);
                        break;
                    case "CheckAbsent":
                        var CheckAbsent = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.CheckAbsent(CheckAbsent);
                        break;
                    case "GetHolidayFromCalendar":
                        var GetHolidayFromCalendar = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.GetHolidayFromCalendar(GetHolidayFromCalendar);
                        break;

                    //APPROVAL
                    case "GetApprovedChangeShift":
                        var GetApprovedChangeShift = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.GetApprovedChangeShift(GetApprovedChangeShift);
                        break;
                    case "GetApprovedOvertime":
                        var GetApprovedOvertime = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.GetApprovedOvertime(GetApprovedOvertime);
                        break;
                    case "CheckLeave":
                        var CheckLeave = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.CheckLeave(CheckLeave);
                        break;
                    case "GetApprovedDtr":
                        var GetApprovedDtr = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.GetApprovedDtr(GetApprovedDtr);
                        break;

                    //Distribute Daily
                    case "GetActualInOut":
                        var GetActualInOut = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.GetActualInOut(GetActualInOut);
                        break;
                    case "ComputeWorkingHourByShift":
                        var ComputeWorkingHourByShift = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.ComputeWorkingHourByShift(ComputeWorkingHourByShift);
                        break;
                    case "ComputeApprovedDtrHourByShift":
                        var ComputeApprovedDtrHourByShift = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.ComputeApprovedDtrHourByShift(ComputeApprovedDtrHourByShift);
                        break;
                    case "ComputeUnderTime":
                        var ComputeUnderTime = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.ComputeUnderTime(ComputeUnderTime);
                        break;
                    case "ComputeLate":
                        var ComputeLate = JsonConvert.DeserializeObject<DTOPayload>(message);
                        repo.ComputeLate(ComputeLate);
                        break;



                    case "BiometricsUpload":
                        var BiometricsUpload = JsonConvert.DeserializeObject<DTOBiometricUpload>(message);
                        repo.UploadFile(BiometricsUpload);
                        break;

                }


            }
            catch (Exception ex)
            {
                _logger.WriteError("Messagetype: " + messageType);
                _logger.WriteError(message, ex);
                throw;
            }

        }
    }
}
