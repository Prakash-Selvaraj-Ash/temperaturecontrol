using System;
using eMTE.Temperature.BusinessLayer.DTO.User.Response;

namespace eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Response
{
    public class GetDashBoardData
    {
        public GetUserResponse User { get; set; }
        public GetDayMeasure DayMeasure { get; set; }
    }
}
