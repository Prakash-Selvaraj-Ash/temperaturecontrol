using System;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.BusinessLayer.Constants;
using eMTE.Temperature.DataAccess.Services;
using eMTE.Temperature.Domain;
using eMTE.Temperature.Service.Contracts;

namespace eMTE.Temperature.Service.Implementation
{
    public class HealthConfigurationService : IHealthConfigurationService
    {
        private IEntityService _entityService;
        private readonly IRepository<HealthMeasureConfiguration> _configRepository;

        public HealthConfigurationService(
            IEntityService entityService,
            IRepository<HealthMeasureConfiguration> configRepository)
        {
            _entityService = entityService;
            _configRepository = configRepository;
        }

        public async Task CreateDefaultConfiguration(Guid organizationId, CancellationToken cancellationToken)
        {
            var configuration = new HealthMeasureConfiguration
            {
                OrganizationId = organizationId,
                MeasureCount = 2,
                TemperatureUnit = Constants.TemperatureUnit.Celcius,
                IsTemperatureMandate = true,
            };

            await _configRepository.CreateAsync(configuration, cancellationToken);
            await _entityService.SaveAsync(cancellationToken);
        }
    }
}
