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

            builder.HasOne(p => p.PaiementDate)
                .WithMany(pd => pd.Paiements)
                .HasForeignKey(p => p.PaiementDateID);

            builder.HasOne(p => p.User)
                .WithMany(u => u.paiements)
                .HasForeignKey(p => p.PaidByUserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
