using System;
using System.Threading;
using System.Threading.Tasks;

namespace eMTE.Temperature.Service.Contracts
{
    public interface IHealthConfigurationService
    {
        Task CreateDefaultConfiguration(Guid organizationId, CancellationToken cancellationToken);
    }
}
