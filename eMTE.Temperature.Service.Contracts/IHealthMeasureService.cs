using System;
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
    }
}
