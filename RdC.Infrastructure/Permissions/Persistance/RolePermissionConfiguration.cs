using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Users;

namespace RdC.Infrastructure.Permissions.Persistance
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(rp => rp.Id);

            builder.HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleID);

            builder.HasOne(rp => rp.PermissionDefinition)
                .WithMany()
                .HasForeignKey(rp => rp.PermissionDefinitionID);

            builder.Property(rp => rp.CanRead).IsRequired();
            builder.Property(rp => rp.CanWrite).IsRequired();
            builder.Property(rp => rp.CanCreate).IsRequired();
        }
    }
}
