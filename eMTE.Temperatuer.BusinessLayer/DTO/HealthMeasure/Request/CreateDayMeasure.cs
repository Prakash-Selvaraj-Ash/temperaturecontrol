using System;
using System.Collections.Generic;

namespace eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Request
{
    public class CreateDayMeasure
    {
        public Guid Id { get; set; }
        public DateTime NotedDate { get; set; }
        public string Intime { get; set; }
        public string OutTime { get; set; }
        public CreateHealthMeasure CreateHealthMeasure { get; set; }
    }
}
