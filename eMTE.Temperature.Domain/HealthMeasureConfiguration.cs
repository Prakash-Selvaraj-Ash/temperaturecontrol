using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class HealthMeasureConfiguration : IDomain, IWithId
    {
        public Guid Id { get; set; }
        [Required]
        public Guid OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }
        public bool IsTemperatureMandate { get; set; }
        [Required]
        public string TemperatureUnit { get; set; }
        public bool IsCoughMandate { get; set; }
        public bool IsSneezingMandate { get; set; }
        public bool IsRunnyNoseMandate { get; set; }
        public bool IsShortnessBreathMandate { get; set; }
        public bool IsOxygenSaturationMandate { get; set; }
        public bool IsHeatRateMandate { get; set; }
        public bool IsImageWithPPEMandate { get; set; }
        [Required]
        public int MeasureCount { get; set; }
    }
}
