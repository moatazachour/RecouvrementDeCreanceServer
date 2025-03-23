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
    [Migration("20250323170927_AddPaiementDates")]
    partial class AddPaiementDates
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Factures_PlanDePaiements", b =>
                {
                    b.Property<int>("PlanID")
                        .HasColumnType("int");

                    b.Property<int>("FactureID")
                        .HasColumnType("int");

                    b.HasKey("PlanID", "FactureID");

                    b.HasIndex("FactureID");

                    b.ToTable("Factures_PlanDePaiements");
                });

            modelBuilder.Entity("RdC.Domain.Acheteurs.Acheteur", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("AcheteurID");

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

                    b.HasKey("Id");

                    b.ToTable("Acheteurs");
                });

            modelBuilder.Entity("RdC.Domain.Factures.Facture", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("FactureID");

                    b.Property<int>("AcheteurID")
                        .HasColumnType("int");

                    b.Property<DateOnly>("DateEcheance")
                        .HasColumnType("date")
                        .HasAnnotation("Relational:JsonPropertyName", "dateDeEcheance");

                    b.Property<decimal>("MontantRestantDue")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("MontantTotal")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<string>("NumFacture")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AcheteurID");

                    b.ToTable("Factures");
                });

            modelBuilder.Entity("RdC.Domain.PaiementDates.PaiementDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DateEcheanceID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("EcheanceDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<int>("PlanID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanID");

                    b.ToTable("PaiementDates");
                });

            modelBuilder.Entity("RdC.Domain.PlanDePaiements.PlanDePaiement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PlanID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("DATETIME");

                    b.Property<decimal>("MontantDeChaqueEcheance")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("MontantRestant")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("MontantTotal")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<byte>("NombreDeEcheances")
                        .HasColumnType("TINYINT");

                    b.Property<byte>("PlanStatus")
                        .HasColumnType("TINYINT");

                    b.HasKey("Id");

                    b.ToTable("PlanDePaiements");
                });

            modelBuilder.Entity("Factures_PlanDePaiements", b =>
                {
                    b.HasOne("RdC.Domain.Factures.Facture", null)
                        .WithMany()
                        .HasForeignKey("FactureID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_FacturePlanDePaiement_Facture");

                    b.HasOne("RdC.Domain.PlanDePaiements.PlanDePaiement", null)
                        .WithMany()
                        .HasForeignKey("PlanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_FacturePlanDePaiement_PlanDePaiement");
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

            modelBuilder.Entity("RdC.Domain.PaiementDates.PaiementDate", b =>
                {
                    b.HasOne("RdC.Domain.PlanDePaiements.PlanDePaiement", "PlanDePaiement")
                        .WithMany("PaiementsDates")
                        .HasForeignKey("PlanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlanDePaiement");
                });

            modelBuilder.Entity("RdC.Domain.Acheteurs.Acheteur", b =>
                {
                    b.Navigation("Factures");
                });

            modelBuilder.Entity("RdC.Domain.PlanDePaiements.PlanDePaiement", b =>
                {
                    b.Navigation("PaiementsDates");
                });
#pragma warning restore 612, 618
        }
    }
}
