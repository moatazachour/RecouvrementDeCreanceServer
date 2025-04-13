using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Relances;

namespace RdC.Infrastructure.Relances.Persistance
{
    public class RelanceConfiguration : IEntityTypeConfiguration<Relance>
    {
        public void Configure(EntityTypeBuilder<Relance> builder)
        {
            builder.ToTable("Relances");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("RelanceID")
                .IsRequired();


            builder.Property(r => r.PaiementDateID)
                .IsRequired();
            
            builder.Property(r => r.IsSent)
                .IsRequired();
            
            builder.Property(r => r.RelanceType)
                .HasConversion<int>()
                .IsRequired();
            
            builder.Property(r => r.DateDeEnvoie)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.HasOne(r => r.PaiementDate)
                .WithMany(pd => pd.Relances)
                .HasForeignKey(r => r.PaiementDateID);
        }
    }
}
