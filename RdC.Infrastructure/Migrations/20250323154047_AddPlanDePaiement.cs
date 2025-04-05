using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanDePaiement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanDePaiements",
                columns: table => new
                {
                    PlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MontantTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    NombreDeEcheances = table.Column<byte>(type: "TINYINT", nullable: false),
                    MontantDeChaqueEcheance = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    MontantRestant = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    PlanStatus = table.Column<byte>(type: "TINYINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanDePaiements", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "Factures_PlanDePaiements",
                columns: table => new
                {
                    PlanID = table.Column<int>(type: "int", nullable: false),
                    FactureID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures_PlanDePaiements", x => new { x.PlanID, x.FactureID });
                    table.ForeignKey(
                        name: "FK_FacturePlanDePaiement_Facture",
                        column: x => x.FactureID,
                        principalTable: "Factures",
                        principalColumn: "FactureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturePlanDePaiement_PlanDePaiement",
                        column: x => x.PlanID,
                        principalTable: "PlanDePaiements",
                        principalColumn: "PlanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factures_PlanDePaiements_FactureID",
                table: "Factures_PlanDePaiements",
                column: "FactureID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factures_PlanDePaiements");

            migrationBuilder.DropTable(
                name: "PlanDePaiements");
        }
    }
}
