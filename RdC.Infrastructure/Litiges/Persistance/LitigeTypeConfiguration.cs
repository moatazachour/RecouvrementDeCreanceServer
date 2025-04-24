using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.Litiges;

namespace RdC.Infrastructure.Litiges.Persistance
{
    public class LitigeTypeConfiguration : IEntityTypeConfiguration<LitigeType>
    {
        public void Configure(EntityTypeBuilder<LitigeType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("LitigeTypeID");

            builder.Property(x => x.Name)
                .HasColumnName("TypeName")
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("TypeDescription")
                .IsRequired();
        }
    }
}
