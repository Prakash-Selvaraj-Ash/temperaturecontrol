using System;
namespace eMTE.Temperature.BusinessLayer.DTO.User.Response
{
    public class GetUserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
