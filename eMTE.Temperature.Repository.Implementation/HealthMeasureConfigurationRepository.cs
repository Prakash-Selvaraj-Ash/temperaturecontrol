using System;
using eMTE.Common.DataAccess;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.Domain;

namespace eMTE.Temperature.Repository.Implementation
{
    public class HealthMeasureConfigurationRepository : RepositoryBase<HealthMeasureConfiguration>, IRepository<HealthMeasureConfiguration>
    {
        public HealthMeasureConfigurationRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
