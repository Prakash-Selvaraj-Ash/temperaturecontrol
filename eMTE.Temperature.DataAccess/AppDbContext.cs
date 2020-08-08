using eMTE.Temperature.Domain;
using Microsoft.EntityFrameworkCore;

namespace eMTE.Temperature.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamUserMap> TeamUserMaps { get; set; }
        public DbSet<DayMeasure> DayMeasures { get; set; }
        public DbSet<HealthMeasure> HealthMeasures { get; set; }
        public DbSet<HealthMeasureConfiguration> HealthMeasureConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<TeamUserMap>(entity =>
            {
                entity.HasIndex(e => new { e.TeamId, e.UserId }).IsUnique();
            });

            modelBuilder.Entity<DayMeasure>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.NotedDate }).IsUnique();
            });

            modelBuilder.Entity<HealthMeasureConfiguration>(entity =>
            {
                entity.HasIndex(e => e.OrganizationId).IsUnique();
            });
        }
    }
}
