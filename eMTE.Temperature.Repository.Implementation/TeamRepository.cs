using System;
using eMTE.Common.DataAccess;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.Domain;

namespace eMTE.Temperature.Repository.Implementation
{
    public class TeamRepository : RepositoryBase<Team>, IRepository<Team>
    {
        public TeamRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
