using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaidByAttributeOnPaiementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaidByUserID",
                table: "Paiements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_PaidByUserID",
                table: "Paiements",
                column: "PaidByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Paiements_Users_PaidByUserID",
                table: "Paiements",
                column: "PaidByUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiements_Users_PaidByUserID",
                table: "Paiements");

            migrationBuilder.DropIndex(
                name: "IX_Paiements_PaidByUserID",
                table: "Paiements");

            migrationBuilder.DropColumn(
                name: "PaidByUserID",
                table: "Paiements");
        }
    }
}
