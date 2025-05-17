using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Litiges;

namespace RdC.Infrastructure.Litiges.Persistance
{
    public class LitigeConfiguration : IEntityTypeConfiguration<Litige>
    {
        public void Configure(EntityTypeBuilder<Litige> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .HasColumnName("LitigeID");

            builder.Property(l => l.FactureID)
                .HasColumnName("FactureID")
                .IsRequired();

            builder.HasOne(l => l.Facture)
                .WithMany(f => f.Litiges)
                .HasForeignKey(l => l.FactureID);

            builder.Property(l => l.LitigeTypeID)
                .HasColumnName("TypeID")
                .IsRequired();

            builder.HasOne(l => l.LitigeType)
                .WithMany(lt => lt.Litiges)
                .HasForeignKey(l => l.LitigeTypeID);

            builder.Property(l => l.LitigeStatus)
                .HasColumnName("LitigeStatus")
                .HasConversion<int>()
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(l => l.LitigeDescription)
                .HasColumnName("LitigeDescription")
                .IsRequired();

            builder.Property(l => l.CreationDate)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(l => l.ResolutionDate)
                .HasColumnType("DATETIME");

            builder.HasOne(l => l.User)
                .WithMany(u => u.litiges)
                .HasForeignKey(l => l.DeclaredByUserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
