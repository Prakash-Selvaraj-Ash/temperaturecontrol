using System;
using System.ComponentModel.DataAnnotations.Schema;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class User : IDomain, IWithId, IWithCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }
        public bool IsOrganizationAdmin { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DisplayPicture { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string RoleDescription { get; set; }
        public bool AlreadyInfected { get; set; }
        public DateTime? InfectedFrom { get; set; }
        public DateTime? InfectedTo { get; set; }

    }
}
