using eMTE.Temperatuer.BusinessLayer.DTO.User.Request;

namespace eMTE.Temperature.Service.Contracts
{
    public interface IUserService
    {
        void CreateUser(CreateUser createUser);
    }
}
