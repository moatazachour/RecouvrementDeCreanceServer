using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFactureStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Factures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Factures");
        }
    }
}
