using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class DayMeasure : IDomain, IWithId
    {
        public Guid Id { get; set; }
        [Required]
        public Guid OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Required]
        public DateTime NotedDate { get; set; }
        public string Intime { get; set; }
        public string OutTime { get; set; }
    }

}
