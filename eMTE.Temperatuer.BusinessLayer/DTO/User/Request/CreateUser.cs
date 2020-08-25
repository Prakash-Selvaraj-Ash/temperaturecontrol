using System;

namespace eMTE.Temperature.BusinessLayer.DTO.User.Request
{
    public class CreateUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public bool IsOrganizationAdmin { get; set; }
        public Guid TeamId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DisplayPicture { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string RoleDescription { get; set; }
    }
}
