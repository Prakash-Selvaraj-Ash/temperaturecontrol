namespace eMTE.Temperature.BusinessLayer.DTO.Organization.Request
{
    public class CreateOrganization
    {
        public string OrganizationName { get; set; }
        public string UserName { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
