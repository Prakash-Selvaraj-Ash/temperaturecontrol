﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class Team : WithAuditFields, IDomain, IWithId
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid TeamManagerId { get; set; }
        [ForeignKey("TeamManagerId")]
        public User TeamManager { get; set; }
        public string TeamDescription { get; set; }
        public string DisplayPicture { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public Guid OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }
    }
}
