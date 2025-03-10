﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RdC.Infrastructure.Common.Persistance;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    [DbContext(typeof(RecouvrementDBContext))]
    [Migration("20250310170957_FactureCreation")]
    partial class FactureCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RdC.Domain.Acheteurs.Acheteur", b =>
                {
                    b.Property<int>("AcheteurID")
                        .HasColumnType("int");

                    b.Property<string>("Adresse")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("AcheteurID");

                    b.ToTable("Acheteurs");
                });

            modelBuilder.Entity("RdC.Domain.Factures.Facture", b =>
                {
                    b.Property<int>("FactureID")
                        .HasColumnType("int");

                    b.Property<int>("AcheteurID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEcheance")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("MontantRestantDue")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("MontantTotal")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<string>("NumFacture")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("FactureID");

                    b.HasIndex("AcheteurID");

                    b.ToTable("Factures");
                });

            modelBuilder.Entity("RdC.Domain.Factures.Facture", b =>
                {
                    b.HasOne("RdC.Domain.Acheteurs.Acheteur", "Acheteur")
                        .WithMany("Factures")
                        .HasForeignKey("AcheteurID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Acheteur");
                });

            modelBuilder.Entity("RdC.Domain.Acheteurs.Acheteur", b =>
                {
                    b.Navigation("Factures");
                });
#pragma warning restore 612, 618
        }
    }
}
