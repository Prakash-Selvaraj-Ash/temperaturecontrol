using System;
using System.Collections.Generic;
using System.Text;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class HealthMeasure : IDomain, IWithId
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime NotedDateTime { get; set; }
        public List<MeasureDetails> MeasureDetails { get; set; }
    }

    public class MeasureDetails
    {
        public string Temperature { get; set; }
        public bool Cough { get; set; }
        public bool Sneezing { get; set; }
        public bool RannyNose { get; set; }
        public bool ShortnessBreath { get; set; }
        public string OxygenSaturation { get; set; }
        public string HeatRate { get; set; }
        public string ImageWithPPE { get; set; }
    }
}
