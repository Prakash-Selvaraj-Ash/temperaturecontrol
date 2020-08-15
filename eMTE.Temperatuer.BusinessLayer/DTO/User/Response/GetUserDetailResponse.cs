using System;
namespace eMTE.Temperature.BusinessLayer.DTO.User.Response
{
    public class GetUserDetailResponse
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public bool IsOrganizationAdmin { get; set; }
        public string Email { get; set; }
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
