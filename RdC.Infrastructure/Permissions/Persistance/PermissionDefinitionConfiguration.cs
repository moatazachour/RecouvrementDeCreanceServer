using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Users;

namespace RdC.Infrastructure.Permissions.Persistance
{
    public class PermissionDefinitionConfiguration : IEntityTypeConfiguration<PermissionDefinition>
    {
        public void Configure(EntityTypeBuilder<PermissionDefinition> builder)
        {
            builder.HasKey(pd => pd.Id);

            builder.Property(pd => pd.Id)
                .HasColumnName("PermissionDefinitionID")
                .ValueGeneratedNever();

            builder.Property(pd => pd.PermissionName)
                .HasColumnName("PermissionName")
                .IsRequired();

            builder.HasData(
                new PermissionDefinition(1, "Gestion des utilisateurs"),
                new PermissionDefinition(2, "Gestion des roles"),
                new PermissionDefinition(3, "Gestion des données de créances (Acheteurs/Factures)"),
                new PermissionDefinition(4, "Gestion des litiges"),
                new PermissionDefinition(5, "Gestion des plan de paiements"),
                new PermissionDefinition(6, "Gestion des paiements"));
        }
    }
}
