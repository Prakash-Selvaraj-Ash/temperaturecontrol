using System;

namespace eMTE.Temperature.BusinessLayer.DTO.User.Request
{
    public class CreateUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsOrganizationAdmin { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
