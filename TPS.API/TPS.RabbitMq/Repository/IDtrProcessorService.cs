using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.DTO;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.RabbitMq.Services
{
    public interface IDtrProcessorService
    {
        //Create q.tps.10 for generation
        void GenerateTimesheet(DTOPayload request);
        void ComputeApprovedOvertimeHr(DTOPayload request);
        void ComputeNightShiftHours(DTOPayload request);
        void GetHolidayFromCalendar(DTOPayload request);
        void CheckAbsent(DTOPayload request);
       
        //APPROVALS
        void GetApprovedChangeShift(DTOPayload request);
        void GetApprovedOvertime(DTOPayload request);
        void CheckLeave(DTOPayload request);
        void GetApprovedDtr(DTOPayload request);


        //Daily LOOp
        void GetActualInOut(DTOPayload payload);
        void ComputeWorkingHourByShift(DTOPayload payload);
        void ComputeApprovedDtrHourByShift(DTOPayload payload);
        void ComputeLate(DTOPayload payload);
        void ComputeUnderTime(DTOPayload payload);

        void ProcessedDTRFlag(DTOPayload request);

        void UploadFile(DTOBiometricUpload dTOBiometricUpload);
    }
}
