using challenge_backend.Core;
using challenge_backend.Domain.Aggregates;
using challenge_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace challenge_backend.PostgreSQL
{
    public class PostgresDbContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly);

            var timezone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");

            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => TimeZoneInfo.ConvertTimeFromUtc(v, timezone)
            );

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var props = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTime));

                foreach (var prop in props)
                {
                    modelBuilder.Entity(entityType.Name)
                        .Property(prop.Name)
                        .HasConversion(dateTimeConverter)
                        .HasColumnType("timestamptz");
                }
            }
        }

        public async Task CommitAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
