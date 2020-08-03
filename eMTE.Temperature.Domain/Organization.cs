using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class Organization : IDomain, IWithId
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Logo { get; set; }
    }
}
