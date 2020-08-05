using System;
using eMTE.Common.DataAccess;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.Domain;

namespace eMTE.Temperature.Repository.Implementation
{
    public class HealthMeasureRepository : RepositoryBase<HealthMeasure>, IRepository<HealthMeasure>
    {
        public HealthMeasureRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
