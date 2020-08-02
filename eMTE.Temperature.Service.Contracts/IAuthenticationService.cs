namespace eMTE.Temperature.Service.Contracts
{
    public interface IAuthenticationService
    {
        string Login(string email, string password);
    }
}
