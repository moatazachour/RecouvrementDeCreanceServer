using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Relances;

namespace RdC.Infrastructure.Relances.Persistance.SMS
{
    public class SMSRelanceConfiguration : IEntityTypeConfiguration<SMSRelance>
    {
        public void Configure(EntityTypeBuilder<SMSRelance> builder)
        {
            builder.ToTable("SMSRelances");

            builder.Property(sr => sr.Telephone)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
