using System;
namespace eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Request
{
    public class GetDashBoardRequest
    {
        public Guid TeamId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
