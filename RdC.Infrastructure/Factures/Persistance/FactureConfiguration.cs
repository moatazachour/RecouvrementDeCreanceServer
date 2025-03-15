using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Factures;

namespace RdC.Infrastructure.Factures.Persistance
{
    public class FactureConfiguration : IEntityTypeConfiguration<Facture>
    {
        public void Configure(EntityTypeBuilder<Facture> builder)
        {
            builder.HasKey(f => f.FactureID);

            builder.Property(f => f.FactureID)
                .ValueGeneratedNever();

            builder.Property(f => f.NumFacture)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(f => f.DateEcheance)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(f => f.MontantTotal)
                .HasColumnType("decimal(18, 3)")
                .IsRequired();

            builder.Property(f => f.MontantRestantDue)
                .HasColumnType("decimal(18, 3)")
                .IsRequired();

            builder.Property(f => f.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(f => f.Acheteur)
                .WithMany(a => a.Factures)
                .HasForeignKey(f => f.AcheteurID);
        }
    }
}
