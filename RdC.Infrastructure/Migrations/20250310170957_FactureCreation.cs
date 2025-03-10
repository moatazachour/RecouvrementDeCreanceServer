using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FactureCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    FactureID = table.Column<int>(type: "int", nullable: false),
                    NumFacture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateEcheance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontantTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    MontantRestantDue = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    AcheteurID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.FactureID);
                    table.ForeignKey(
                        name: "FK_Factures_Acheteurs_AcheteurID",
                        column: x => x.AcheteurID,
                        principalTable: "Acheteurs",
                        principalColumn: "AcheteurID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factures_AcheteurID",
                table: "Factures",
                column: "AcheteurID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factures");
        }
    }
}
