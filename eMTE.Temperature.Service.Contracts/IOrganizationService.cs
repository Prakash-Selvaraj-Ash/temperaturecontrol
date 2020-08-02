using System;
using eMTE.Temperature.BusinessLayer.DTO.Organization.Request;

namespace eMTE.Temperature.Service.Contracts
{
    public interface IOrganizationService
    {
        void RegisterOrganization(CreateOrganization createOrganization);
    }
}
