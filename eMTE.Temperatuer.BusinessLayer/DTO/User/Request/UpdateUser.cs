using System;
namespace eMTE.Temperature.BusinessLayer.DTO.User.Request
{
    public class UpdateUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DisplayPicture { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string RoleDescription { get; set; }
    }
}
