using System;
namespace eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Request
{
    public class GetExportRequest
    {
        public Guid TeamId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
