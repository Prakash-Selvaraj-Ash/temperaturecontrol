using System;
using eMTE.Common.DataAccess;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.Domain;

namespace eMTE.Temperature.Repository.Implementation
{
    public class DayMeasureRepository : RepositoryBase<DayMeasure>, IRepository<DayMeasure>
    {
        public DayMeasureRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
