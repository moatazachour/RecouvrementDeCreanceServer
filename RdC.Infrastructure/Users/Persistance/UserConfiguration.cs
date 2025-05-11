using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Users;

namespace RdC.Infrastructure.Users.Persistance
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("UserID");

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID);

            builder.Property(u => u.Status)
                .HasConversion<int>()
                .HasColumnType("TINYINT")
                .IsRequired();
        }
    }
}
