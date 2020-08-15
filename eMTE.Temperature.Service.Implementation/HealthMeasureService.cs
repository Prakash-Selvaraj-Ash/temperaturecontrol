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
using eMTE.Common.Export.ExcelExport;
using eMTE.Common.Export.ExcelExport.DTO;
using System.Collections.Generic;

namespace eMTE.Temperature.Service.Implementation
{
    public class HealthMeasureService : IHealthMeasureService
    {
        private class ExportDbContext
        {
            public User User { get; set; }
            public DayMeasure DayMeasure { get; set; }
            public HealthMeasure HealthMeasure { get; set; }
        }

        private class ExportContext
        {
            public User User { get; set; }
            public IEnumerable<MeasureContext> Days { get; set; }
        }

        private class MeasureContext
        {
            public DayMeasure Day { get; set; }
            public IEnumerable<HealthMeasure> HealthMeasures { get; set; }
        }

        

        private readonly IUserResolver _userResolver;
        private readonly IEntityService _entityService;
        private readonly IExcelExportService _excelExportService;

        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<DayMeasure> _dayMeasureRepository;
        private readonly IRepository<TeamUserMap> _teamUserMapRepository;
        private readonly IRepository<HealthMeasure> _healthMeasureRepository;
        IRepository<HealthMeasureConfiguration> _healthMeasureConfigurationRepository;

        public HealthMeasureService(
            IUserResolver userResolver,
            IEntityService entityService,
            IExcelExportService excelExportService,
            IRepository<Team> teamRepository,
            IRepository<DayMeasure> dayMeasureRepository,
            IRepository<TeamUserMap> teamUserMapRepository,
            IRepository<HealthMeasure> healthMeasureRepository,
            IRepository<HealthMeasureConfiguration> healthMeasureConfigurationRepository)
        {
            _userResolver = userResolver;
            _entityService = entityService;
            _teamUserMapRepository = teamUserMapRepository;

            _teamRepository = teamRepository;
            _excelExportService = excelExportService;
            _dayMeasureRepository = dayMeasureRepository;
            _healthMeasureRepository = healthMeasureRepository;
            _healthMeasureConfigurationRepository = healthMeasureConfigurationRepository;
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

        public async Task<byte[]> Export(Guid teamId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            var rows = await GetInnerExport(teamId, startDate, endDate, cancellationToken);

            return _excelExportService.Export(rows);
        }

        private async Task<IEnumerable<Row>> GetInnerExport(Guid teamId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            var slotCount = await GetSlotCount(teamId, cancellationToken);
            var dbResult = await GetExportContexts(teamId, startDate, endDate, cancellationToken);

            var groupedResult = dbResult
                .GroupBy(res => res.User.Id)
                .Select(group => new ExportContext
                {
                    User = group.First().User,
                    Days = group
                        .GroupBy(g => g.DayMeasure.Id)
                        .Select(gDate => new MeasureContext
                        {
                            Day = gDate.First().DayMeasure,
                            HealthMeasures = gDate.Select(measure => measure.HealthMeasure)
                        })
                });

            var daysToGenerate = Enumerable.Range(0, 1 + endDate.Subtract(startDate).Days)
              .Select(offset => startDate.AddDays(offset))
              .ToArray();

            var rows = GetInnerExportRows(slotCount, daysToGenerate, groupedResult);

            return rows;
        }

        private IEnumerable<Row> GetInnerExportRows(int slotCount, DateTime[] daysToGenerate, IEnumerable<ExportContext> exportContexts)
        {
            var rows = new List<Row>();
            var headers = GetHeaders(slotCount, daysToGenerate);

            var contentRows = exportContexts.Select(res =>
            {
                var cells = new List<Column>();
                var row = new Row { };
                var userNameCell = new Column(1, 1, res.User.Name, false);
                var daysCell = daysToGenerate.SelectMany((day, index) =>
                {
                    var startIndex = (index * slotCount) + 2;
                    var existingDayMeasure = res.Days.FirstOrDefault(dbDate => dbDate.Day.NotedDate.Date.Equals(day.Date));
                    if (existingDayMeasure != null)
                    {
                        var columns = new List<Column>();
                        for (int i = 0; i < slotCount; i++)
                        {
                            var existingSlot = existingDayMeasure.HealthMeasures.FirstOrDefault(slot => slot.SlotNumber == i + 1);
                            if (existingSlot != null)
                            {
                                columns.Add(new Column(startIndex + i, startIndex + i, $"{existingSlot.Temperature}-{existingSlot.TemperatureUnit}", false));
                            }
                            else
                            {
                                columns.Add(new Column(startIndex + i, startIndex + i, string.Empty, false));
                            }
                        }
                        return columns;
                    }
                    else
                    {
                        return Enumerable.Range(0, slotCount).Select(i => new Column((startIndex + i), (startIndex + i), string.Empty, false)).ToList();
                    }
                }).ToArray();
                cells.Add(userNameCell);
                cells.AddRange(daysCell);

                row.Cells = cells;
                return row;
            }).ToArray();

            rows.AddRange(headers);
            rows.AddRange(contentRows);

            return rows;
        }

        private async Task<int> GetSlotCount(Guid teamId, CancellationToken cancellationToken)
        {
            var configuration =
                await(from team in _teamRepository.Set.Where(t => t.Id == teamId)

                      join healthConfiguartion in _healthMeasureConfigurationRepository.Set
                      on team.OrganizationId equals healthConfiguartion.OrganizationId

                      select healthConfiguartion.MeasureCount).ToArrayAsync(cancellationToken);

            var slotCount = configuration.First();

            return slotCount;
        }

        private async Task<ExportDbContext[]> GetExportContexts(
            Guid teamId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            var asyncResult =
                from teamUserMap in _teamUserMapRepository.Set
                    .Include(map => map.User)
                    .Where(map => map.TeamId == teamId)

                join dayMeasure in _dayMeasureRepository.Set
                    .Where(measure => measure.NotedDate.Date >= startDate.Date && measure.NotedDate.Date <= endDate.Date)
                on teamUserMap.UserId equals dayMeasure.UserId

                join healthMeasure in _healthMeasureRepository.Set
                on dayMeasure.Id equals healthMeasure.DayMeasureId

                select new ExportDbContext
                {
                    User = teamUserMap.User,
                    HealthMeasure = healthMeasure,
                    DayMeasure = dayMeasure
                };

            var dbResult = await asyncResult.ToArrayAsync(cancellationToken);
            return dbResult;
        }

        private IEnumerable<Row> GetHeaders(int slotCount, DateTime[] daysToGenerate)
        {
            var rows = new List<Row>();

            var mainColumns = new List<Column>();
            mainColumns.Add(new Column(1, 1, "UserName"));
            var currentColumnCount = 2;
            foreach (var day in daysToGenerate)
            {
                var endIndex = currentColumnCount + slotCount - 1;
                mainColumns.Add(new Column(currentColumnCount, endIndex, day.ToShortDateString(), true));
                currentColumnCount = endIndex + 1;
            }


            rows.Add(new Row { Cells = mainColumns });

            var subHeaderColumns = new List<Column>();
            subHeaderColumns.Add(new Column(1, 1, string.Empty));
            var subHeaderStartColIndex = 2;
            foreach (var day in daysToGenerate)
            {
                for (int i = 1; i <= slotCount; i++)
                {
                    subHeaderColumns.Add(new Column(subHeaderStartColIndex, subHeaderStartColIndex, $"Slot {i}"));
                    subHeaderStartColIndex++;
                }
            }

            rows.Add(new Row { Cells = subHeaderColumns });

            return rows;
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
                    HealthMeasures = r.Select(_r => _r.healthMeasure).ToList<GetHealthMeasure>()
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

        public async Task<IEnumerable<ExportRow>> GetExportRows(Guid teamId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            var rows = await GetInnerExport(teamId, startDate, endDate, cancellationToken);
            var result = rows.ToList<ExportRow>();
            return result;
        }
    }
}
