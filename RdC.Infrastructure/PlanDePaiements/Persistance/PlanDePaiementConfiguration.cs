using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Factures;
using RdC.Domain.PlanDePaiements;

namespace RdC.Infrastructure.PlanDePaiements.Persistance
{
    public class PlanDePaiementConfiguration : IEntityTypeConfiguration<PlanDePaiement>
    {
        public void Configure(EntityTypeBuilder<PlanDePaiement> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("PlanID");

            builder.Property(p => p.MontantTotal)
                .HasColumnType("decimal(18, 3)")
                .IsRequired();

            builder.Property(p => p.NombreDeEcheances)
                .HasColumnType("TINYINT")
                .IsRequired();

            builder.Property(p => p.MontantRestant)
                .HasColumnType("decimal(18, 3)")
                .IsRequired();

            builder.Property(p => p.CreationDate)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(p => p.PlanStatus)
                .HasConversion<int>()
                .HasColumnType("TINYINT")
                .IsRequired();

            builder.Property(p => p.IsLocked)
                .HasColumnType("BIT")
                .IsRequired();

            builder.Property(p => p.HasAdvance)
                .HasColumnType("BIT")
                .IsRequired();

            builder.Property(p => p.ValidationDate)
                .HasColumnType("DATETIME");

            builder.HasOne(p => p.User)
                .WithMany(u => u.planDePaiements)
                .HasForeignKey(p => p.CreatedByUserID);

            builder.HasMany(plan => plan.Factures)
                .WithMany(facture => facture.PlanDePaiements)
                .UsingEntity("Factures_PlanDePaiements",
                    left => left.HasOne(typeof(Facture))
                        .WithMany()
                        .HasForeignKey("FactureID")
                        .HasPrincipalKey(nameof(Facture.Id))
                        .HasConstraintName("FK_FacturePlanDePaiement_Facture")
                        .OnDelete(DeleteBehavior.Cascade),
                    right => right.HasOne(typeof(PlanDePaiement))
                        .WithMany()
                        .HasForeignKey("PlanID")
                        .HasPrincipalKey(nameof(PlanDePaiement.Id))
                        .HasConstraintName("FK_FacturePlanDePaiement_PlanDePaiement")
                        .OnDelete(DeleteBehavior.Cascade),
                    linkBuilder => linkBuilder.HasKey("PlanID", "FactureID")
                );
        }
    }
}
