using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Common.Authentication;
using eMTE.Common.Domain;
using eMTE.Common.Tools.Contract;
using Microsoft.EntityFrameworkCore.Storage;

namespace eMTE.Temperature.DataAccess.Services
{
    public class EntityService : IEntityService
    {
        private readonly AppDbContext _context;
        private readonly IUserResolver _userResolver;
        private readonly IDateTimeToolService _dateTimeToolService;

        public EntityService(
            AppDbContext dbContext,
            IUserResolver userResolver,
            IDateTimeToolService dateTimeToolService)
        {
            _context = dbContext;
            _userResolver = userResolver;
            _dateTimeToolService = dateTimeToolService;
        }

        public IDbContextTransaction GetOrBeginTransaction()
        {
            return _context.Database.CurrentTransaction ?? _context.Database.BeginTransaction();
        }

        public void Save()
        {
            ConsiderSetAuditFields();
            _context.SaveChanges();
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            ConsiderSetAuditFields();
            await _context.SaveChangesAsync(cancellationToken);
        }

        private void ConsiderSetAuditFields()
        {
            var entries = _context.ChangeTracker.Entries()
                .Where(entry =>
                    entry.Entity is IWithAuditFields &&
                    (entry.State == Microsoft.EntityFrameworkCore.EntityState.Added ||
                    entry.State == Microsoft.EntityFrameworkCore.EntityState.Modified));
            
            if (!entries.Any()) { return;  }

            string claimUserId = _userResolver.TryGetUserId();

            if(string.IsNullOrWhiteSpace(claimUserId)) { return; }

            var userId = Guid.Parse(claimUserId);
            foreach (var entry in entries)
            {
                var auditFieldEntity = entry.Entity as IWithAuditFields;
                if(auditFieldEntity.CreatedById == Guid.Empty)
                {
                    auditFieldEntity.CreatedById = userId;
                    auditFieldEntity.CreatedTime = _dateTimeToolService.DateTime;
                }
                else
                {
                    auditFieldEntity.ModifiedById = userId;
                    auditFieldEntity.ModifiedTime = _dateTimeToolService.DateTime;
                }
            }
        }
    }
}
