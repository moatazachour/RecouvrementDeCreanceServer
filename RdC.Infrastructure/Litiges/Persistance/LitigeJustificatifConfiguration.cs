using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Litiges;

namespace RdC.Infrastructure.Litiges.Persistance
{
    public class LitigeJustificatifConfiguration : IEntityTypeConfiguration<LitigeJustificatif>
    {
        public void Configure(EntityTypeBuilder<LitigeJustificatif> builder)
        {
            builder.HasKey(lj => lj.Id);

            builder.Property(lj => lj.Id)
                .HasColumnName("JustificationID");

            builder.Property(lj => lj.LitigeID)
                .HasColumnName("LitigeID")
                .IsRequired();

            builder.HasOne(lj => lj.Litige)
                .WithMany(l => l.Justificatifs)
                .HasForeignKey(l => l.LitigeID);

            builder.Property(lj => lj.NomFichier)
                .HasColumnName("NomFicher")
                .IsRequired();

            builder.Property(lj => lj.CheminFichier)
               .HasColumnName("CheminFichier")
               .IsRequired();

            builder.Property(lj => lj.DateAjout)
               .HasColumnName("DateAjout")
               .HasColumnType("DATETIME")
               .IsRequired();
        }
    }
}
