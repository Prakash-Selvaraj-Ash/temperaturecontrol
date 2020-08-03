using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace eMTE.Temperature.DataAccess.Services
{
    public interface IEntityService
    {
        IDbContextTransaction GetOrBeginTransaction();

        void Save();

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
