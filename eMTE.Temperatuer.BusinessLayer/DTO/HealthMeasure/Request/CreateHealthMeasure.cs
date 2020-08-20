using System;
namespace eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Request
{
    public class CreateHealthMeasure
    {
        public double Temperature { get; set; }
        public string TemperatureUnit { get; set; }
        public int SlotNumber { get; set; }
        public bool Cough { get; set; }
        public bool Sneezing { get; set; } // app mapping: soreThroat
        public bool RunnyNose { get; set; } // app mapping: cold
        public bool ShortnessBreath { get; set; }
        public string OxygenSaturation { get; set; }
        public string HeatRate { get; set; }
        public string ImageWithPPE { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
