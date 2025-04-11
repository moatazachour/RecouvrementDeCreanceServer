using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationBetweenPaiementsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiements_PlanDePaiements_PlanID",
                table: "Paiements");

            migrationBuilder.RenameColumn(
                name: "PlanID",
                table: "Paiements",
                newName: "PaiementDateID");

            migrationBuilder.RenameIndex(
                name: "IX_Paiements_PlanID",
                table: "Paiements",
                newName: "IX_Paiements_PaiementDateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Paiements_PaiementDates_PaiementDateID",
                table: "Paiements",
                column: "PaiementDateID",
                principalTable: "PaiementDates",
                principalColumn: "DateEcheanceID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiements_PaiementDates_PaiementDateID",
                table: "Paiements");

            migrationBuilder.RenameColumn(
                name: "PaiementDateID",
                table: "Paiements",
                newName: "PlanID");

            migrationBuilder.RenameIndex(
                name: "IX_Paiements_PaiementDateID",
                table: "Paiements",
                newName: "IX_Paiements_PlanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Paiements_PlanDePaiements_PlanID",
                table: "Paiements",
                column: "PlanID",
                principalTable: "PlanDePaiements",
                principalColumn: "PlanID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
