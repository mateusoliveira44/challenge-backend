using challenge_backend.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace challenge_backend.PostgreSQL.EntityTypeConfigurations
{
    public class WalletEntityTypeConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("wallets");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(x => x.Balance)
                .HasColumnName("balance")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasIndex(x => x.UserId)
                .IsUnique();
        }
    }
}
