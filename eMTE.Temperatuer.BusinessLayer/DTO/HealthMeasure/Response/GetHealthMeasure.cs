using System;
namespace eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Response
{
    public class GetHealthMeasure
    {
        public double Temperature { get; set; }
        public string TemperatureUnit { get; set; }
        public bool Cough { get; set; }
        public bool Sneezing { get; set; }
        public bool RunnyNose { get; set; }
        public bool ShortnessBreath { get; set; }
        public string OxygenSaturation { get; set; }
        public string HeatRate { get; set; }
        public string ImageWithPPE { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
