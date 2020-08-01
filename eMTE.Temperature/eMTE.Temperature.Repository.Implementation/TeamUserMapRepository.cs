using System;
using eMTE.Common.DataAccess;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.Domain;

namespace eMTE.Temperature.Repository.Implementation
{
    public class TeamUserMapRepository : RepositoryBase<TeamUserMap>, IRepository<TeamUserMap>
    {
        public TeamUserMapRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
