using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaiementInfoToPaiementDateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "PaiementDates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MontantDeEcheance",
                table: "PaiementDates",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MontantDue",
                table: "PaiementDates",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MontantPayee",
                table: "PaiementDates",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "PaiementDates");

            migrationBuilder.DropColumn(
                name: "MontantDeEcheance",
                table: "PaiementDates");

            migrationBuilder.DropColumn(
                name: "MontantDue",
                table: "PaiementDates");

            migrationBuilder.DropColumn(
                name: "MontantPayee",
                table: "PaiementDates");
        }
    }
}
