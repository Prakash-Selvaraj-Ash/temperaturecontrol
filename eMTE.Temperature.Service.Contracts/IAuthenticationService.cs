using System.Threading;
using System.Threading.Tasks;

namespace eMTE.Temperature.Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<string> Login(string email, string password, CancellationToken cancellationToken);
    }
}
