using System;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Temperature.BusinessLayer.DTO.Organization.Request;

namespace eMTE.Temperature.Service.Contracts
{
    public interface IOrganizationService
    {
        Task RegisterOrganization(CreateOrganization createOrganization, CancellationToken cancellationToken);
    }
}
