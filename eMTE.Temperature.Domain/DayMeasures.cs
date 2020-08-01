using System;
using System.Collections.Generic;
using System.Text;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class DayMeasures : IDomain, IWithId
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime NotedDate { get; set; }
        public string Intime { get; set; }
        public string OutTime { get; set; }
        public List<HealthMeasure> MeasureDetails { get; set; }
    }

}
