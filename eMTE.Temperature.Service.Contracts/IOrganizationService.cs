using System;
using eMTE.Temperatuer.BusinessLayer.DTO.Organization.Request;

namespace eMTE.Temperature.Service.Contracts
{
    public interface IOrganizationService
    {
        void RegisterOrganization(CreateOrganization createOrganization);
    }
}
