using System.Threading;
using System.Threading.Tasks;
using eMTE.Common.Domain;

namespace eMTE.Common.Repository.Contracts
{
    public interface IWithCodeRepository<TDomain> : IRepository<TDomain>
        where TDomain : class, IDomain, IWithCode
    {
        Task<TDomain> ReadByCodeAsync(string code, CancellationToken cancellationToken);
    }
}
