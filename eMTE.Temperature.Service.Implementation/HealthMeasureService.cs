using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using eMTE.Common.Authentication;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.BusinessLayer.Constants;
using eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Request;
using eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Response;
using eMTE.Temperature.BusinessLayer.Extensions;
using eMTE.Temperature.DataAccess.Services;
using eMTE.Temperature.Domain;
using eMTE.Temperature.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace eMTE.Temperature.Service.Implementation
{
    public class HealthMeasureService : IHealthMeasureService
    {
        private readonly IUserResolver _userResolver;
        private readonly IEntityService _entityService;
        private readonly IRepository<DayMeasure> _dayMeasureRepository;
        private readonly IRepository<HealthMeasure> _healthMeasureRepository;

        public HealthMeasureService(
            IUserResolver userResolver,
            IEntityService entityService,
            IRepository<DayMeasure> dayMeasureRepository,
            IRepository<HealthMeasure> healthMeasureRepository)
        {
            _userResolver = userResolver;
            _entityService = entityService;
            _dayMeasureRepository = dayMeasureRepository;
            _healthMeasureRepository = healthMeasureRepository;
        }

        public async Task CreateMeasure(CreateDayMeasure createDayMeasure, CancellationToken cancellationToken)
        {
            var dayMeasure = await CreateOrUpdateDayMeasure(createDayMeasure, cancellationToken);

            if(createDayMeasure.CreateHealthMeasure != null)
            {
                var healthMeasure = createDayMeasure.CreateHealthMeasure.To<HealthMeasure>();
                healthMeasure.DayMeasureId = dayMeasure.Id;

                await _healthMeasureRepository.CreateAsync(healthMeasure, cancellationToken);
            }

            await _entityService.SaveAsync(cancellationToken);
        }

        public async Task<GetDayMeasure> GetDayMeasure(DateTime date, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_userResolver.GetUserId());
            var asyncResult =
                from dayMeasure in _dayMeasureRepository.Set
                    .Where(measure => measure.NotedDate.Date == date.Date && measure.UserId == userId)

                join healthMeasure in _healthMeasureRepository.Set
                on dayMeasure.Id equals healthMeasure.DayMeasureId

                select new
                {
                    dayMeasure.Id,
                    dayMeasure,
                    healthMeasure
                };

            var result = await asyncResult.ToArrayAsync(cancellationToken);

            var response = result
                .GroupBy(r => r.Id)
                .Select(r => new GetDayMeasure
                {
                    Id = r.Key,
                    Intime = r.First().dayMeasure.Intime,
                    OutTime = r.First().dayMeasure.OutTime,
                    NotedDate = r.First().dayMeasure.NotedDate,
                    HealthMeasures = r.Select(r => r.healthMeasure).ToList<GetHealthMeasure>()
                });

            return response.FirstOrDefault();
        }

        private async Task<DayMeasure> CreateOrUpdateDayMeasure(CreateDayMeasure createDayMeasure, CancellationToken cancellationToken)
        {
            var userId = _userResolver.GetUserId();
            var organizationId = _userResolver.GetClaimIdentifierValue(Constants.Claims.Organization);
            Guid dayMeasureId = createDayMeasure.Id != Guid.Empty
                ? createDayMeasure.Id
                : Guid.NewGuid();

            if (createDayMeasure.Id == Guid.Empty)
            {
                var dayMeasure = createDayMeasure.To<DayMeasure>();
                dayMeasure.Id = dayMeasureId;
                dayMeasure.UserId = Guid.Parse(userId);
                dayMeasure.OrganizationId = Guid.Parse(organizationId);
                await _dayMeasureRepository.CreateAsync(dayMeasure, cancellationToken);

                return dayMeasure;
            }
            else
            {
                var dayMeasure = await _dayMeasureRepository.ReadByIdAsync(dayMeasureId, cancellationToken);
                createDayMeasure.Into(dayMeasure);

                return dayMeasure;
            }
        }
    }
}
