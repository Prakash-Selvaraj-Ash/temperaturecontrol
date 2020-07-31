using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class Team : IDomain, IWithId
    {
        public Guid Id { get; set; }
        public Guid TeamManagerId { get; set; }
        [ForeignKey("TeamManagerId")]
        public User TeamManager { get; set; }
    }
}
