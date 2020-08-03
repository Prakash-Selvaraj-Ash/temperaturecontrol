using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace eMTE.Temperature.DataAccess.Services
{
    public class EntityService : IEntityService
    {
        private readonly AppDbContext _context;
        public EntityService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public IDbContextTransaction GetOrBeginTransaction()
        {
            return _context.Database.CurrentTransaction ?? _context.Database.BeginTransaction();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
