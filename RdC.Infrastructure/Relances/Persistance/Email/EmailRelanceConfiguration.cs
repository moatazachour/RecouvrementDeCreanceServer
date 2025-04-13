using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Relances;

namespace RdC.Infrastructure.Relances.Persistance.Email
{
    public class EmailRelanceConfiguration : IEntityTypeConfiguration<EmailRelance>
    {
        public void Configure(EntityTypeBuilder<EmailRelance> builder)
        {
            builder.ToTable("EmailRelances");

            builder.Property(er => er.Email)
                .HasMaxLength(200)
                .IsRequired();

        }
    }
}
