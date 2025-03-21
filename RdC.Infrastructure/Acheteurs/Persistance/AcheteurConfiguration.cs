using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Acheteurs;

namespace RdC.Infrastructure.Acheteurs.Persistance
{
    public class AcheteurConfiguration : IEntityTypeConfiguration<Acheteur>
    {
        public void Configure(EntityTypeBuilder<Acheteur> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("AcheteurID");

            builder.Property(a => a.Id)
                .ValueGeneratedNever();

            builder.Property(a => a.Nom)
                .HasMaxLength(100);

            builder.Property(a => a.Prenom)
                .HasMaxLength(100);

            builder.Property(a => a.Adresse)
                .HasMaxLength(500);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Telephone)
                .HasMaxLength(20);
        }
    }
}
