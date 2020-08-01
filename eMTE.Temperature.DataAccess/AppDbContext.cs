using eMTE.Common.DataAccess;
using eMTE.Temperature.Domain;
using Microsoft.EntityFrameworkCore;

namespace eMTE.Temperature.DataAccess
{
    public class AppDbContext : BaseDbContext
    {
        public AppDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamUserMap> TeamUserMaps { get; set; }
    }
}
