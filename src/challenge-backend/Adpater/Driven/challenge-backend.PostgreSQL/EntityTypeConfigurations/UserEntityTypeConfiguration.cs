using challenge_backend.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace challenge_backend.PostgreSQL.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(255)
                .IsRequired();

            builder.OwnsOne(x => x.Email, navigationBuilder =>
            {
                navigationBuilder.Property(m => m.MailAddress)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsRequired();

                navigationBuilder.HasIndex(m => m.MailAddress)
                    .IsUnique();
            });
        }
    }
}
