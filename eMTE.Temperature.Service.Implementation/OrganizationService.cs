using System.Threading;
using System.Threading.Tasks;
using eMTE.Common.Authentication;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.BusinessLayer.DTO.Organization.Request;
using eMTE.Temperature.BusinessLayer.Extensions;
using eMTE.Temperature.DataAccess.Services;
using eMTE.Temperature.Domain;
using eMTE.Temperature.Service.Contracts;
using static eMTE.Temperature.BusinessLayer.Constants.Constants;

namespace eMTE.Temperature.Service.Implementation
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Organization> _organizationRepository;

        private readonly IEntityService _entityService;
        private readonly IHealthConfigurationService _healthConfigurationService;

        public OrganizationService(
            IAuthenticator authenticator,
            IEntityService entityService,
            IRepository<User> userRepository,
            IRepository<Organization> organizationRepository,
            IHealthConfigurationService healthConfigurationService)
        {
            _authenticator = authenticator;
            _entityService = entityService;
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;
            _healthConfigurationService = healthConfigurationService;
        }

        public async Task RegisterOrganization(CreateOrganization createOrganization, CancellationToken cancellationToken)
        {
            var organization = createOrganization.To<Organization>();
            var user = createOrganization.To<User>();
            user.OrganizationId = organization.Id;


            var authModel = _authenticator.Create(Secret.PasswordKey, createOrganization.Password);
            user.Hash = authModel.Hash;
            user.IsOrganizationAdmin = true;


            await _organizationRepository.CreateAsync(organization, cancellationToken);
            await _userRepository.CreateAsync(user, cancellationToken);

            await _healthConfigurationService.CreateDefaultConfiguration(organization.Id, cancellationToken);

            await _entityService.SaveAsync(cancellationToken);
        }
    }
}
