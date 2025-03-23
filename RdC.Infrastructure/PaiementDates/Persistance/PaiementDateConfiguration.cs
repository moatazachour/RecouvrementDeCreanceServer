﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RdC.Domain.PaiementDates;

namespace RdC.Infrastructure.PaiementDates.Persistance
{
    public class PaiementDateConfiguration : IEntityTypeConfiguration<PaiementDate>
    {
        public void Configure(EntityTypeBuilder<PaiementDate> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .HasColumnName("DateEcheanceID");

            builder.Property(d => d.EcheanceDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(d => d.IsPaid)
                .HasColumnType("bit")
                .IsRequired();

            builder.HasOne(d => d.PlanDePaiement)
                .WithMany(p => p.PaiementsDates)
                .HasForeignKey(d => d.PlanID);
        }
    }
}
