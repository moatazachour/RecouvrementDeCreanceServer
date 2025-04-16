using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlanDePaiement_AddIsLocked_RemoveMontantDeChaqueEcheance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontantDeChaqueEcheance",
                table: "PlanDePaiements");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "PlanDePaiements",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "PlanDePaiements");

            migrationBuilder.AddColumn<decimal>(
                name: "MontantDeChaqueEcheance",
                table: "PlanDePaiements",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
