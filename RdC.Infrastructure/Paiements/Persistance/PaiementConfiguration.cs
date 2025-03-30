using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Paiements;

namespace RdC.Infrastructure.Paiements.Persistance
{
    public class PaiementConfiguration : IEntityTypeConfiguration<Paiement>
    {
        public void Configure(EntityTypeBuilder<Paiement> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
               .HasColumnName("PaiementID");

            builder.Property(p => p.MontantPayee)
                .HasColumnType("decimal(18, 3)")
                .IsRequired();

            builder.Property(p => p.DateDePaiement)
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(p => p.PlanDePaiement)
                .WithMany(pp => pp.Paiements)
                .HasForeignKey(p => p.PlanID);
        }
    }
}
