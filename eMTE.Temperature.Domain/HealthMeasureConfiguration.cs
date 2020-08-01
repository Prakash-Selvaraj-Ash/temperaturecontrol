using System;
using System.Collections.Generic;
using System.Text;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class HealthMeasureConfiguration
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public bool IsTemperatureMandate { get; set; }
        public string CelciusOrFarenheat { get; set; }
        public bool IsCoughMandate { get; set; }
        public bool IsSneezingMandate { get; set; }
        public bool IsRunnyNoseMandate { get; set; }
        public bool IsShortnessBreathMandate { get; set; }
        public bool IsOxygenSaturationMandate { get; set; }
        public bool IsHeatRateMandate { get; set; }
        public bool IsImageWithPPEMandate { get; set; }
    }
}
