using System;
using System.Collections.Generic;

namespace eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Response
{
    public class GetDayMeasure
    {
        public Guid Id { get; set; }
        public DateTime NotedDate { get; set; }
        public string Intime { get; set; }
        public string OutTime { get; set; }
        public IEnumerable<GetHealthMeasure> HealthMeasures { get; set; }
    }
}
