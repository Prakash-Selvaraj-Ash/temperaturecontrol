using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class TeamUserMap : IDomain, IWithId
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
