using System;
using System.Collections.Generic;
using System.Text;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class HealthMeasure
    {
        public string Temperature { get; set; }
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
