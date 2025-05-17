using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Users;

namespace RdC.Infrastructure.Roles.Persistance
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(pd => pd.Id);

            builder.Property(pd => pd.Id)
                .HasColumnName("RoleID");

            builder.Property(pd => pd.RoleName)
                .HasColumnName("RoleName")
                .IsRequired();
        }
    }
}
