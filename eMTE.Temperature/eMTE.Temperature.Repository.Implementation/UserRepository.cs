using System;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.Domain;
using eMTE.Common.DataAccess;

namespace eMTE.Temperature.Repository.Implementation
{
    public class UserRepository : RepositoryBase<User>, IRepository<User>
    {
        public UserRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
