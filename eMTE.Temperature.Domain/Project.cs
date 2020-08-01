using System;
using System.Collections.Generic;
using System.Text;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class Project : IDomain, IWithId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
