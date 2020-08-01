using eMTE.Common.DataAccess;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.Domain;

namespace eMTE.Temperature.Repository.Implementation
{
    public class OrganizationRepository : RepositoryBase<Organization>, IRepository<Organization>
    {
        public OrganizationRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
