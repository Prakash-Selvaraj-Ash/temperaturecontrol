using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Request;
using eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Response;

namespace eMTE.Temperature.Service.Contracts
{
    public interface IHealthMeasureService
    {
        Task<GetDayMeasure> GetDayMeasure(DateTime date, CancellationToken cancellationToken);
        Task CreateMeasure(CreateDayMeasure createDayMeasure, CancellationToken cancellationToken);
        Task<byte[]> Export(Guid teamId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
        Task<IEnumerable<ExportRow>> GetExportRows(Guid teamId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
