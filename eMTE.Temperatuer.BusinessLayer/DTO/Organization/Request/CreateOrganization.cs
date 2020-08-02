namespace eMTE.Temperature.BusinessLayer.DTO.Organization.Request
{
    public class CreateOrganization
    {
        public string Name { get; set; }
        public string LogoImageString { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
