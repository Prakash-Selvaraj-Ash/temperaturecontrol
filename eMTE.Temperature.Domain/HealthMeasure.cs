using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class HealthMeasure : IDomain, IWithId
    {
        public Guid Id { get; set; }
        [Required]
        public double Temperature { get; set; }
        [Required]
        public string TemperatureUnit { get; set; }
        public bool Cough { get; set; }
        public bool Sneezing { get; set; }
        public bool RunnyNose { get; set; }
        public bool ShortnessBreath { get; set; }
        public string OxygenSaturation { get; set; }
        public string HeatRate { get; set; }
        public string ImageWithPPE { get; set; }
        [Required]
        public int SlotNumber { get; set; }
        [Required]
        public DateTime UpdateDateTime { get; set; }
        [Required]
        public Guid DayMeasureId { get; set; }
        [ForeignKey("DayMeasureId")]
        public virtual DayMeasure DayMeasure { get; set; }
    }
   
}
