using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeletionTypeOnPlanDePaiementUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanDePaiements_Users_CreatedByUserID",
                table: "PlanDePaiements");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanDePaiements_Users_CreatedByUserID",
                table: "PlanDePaiements",
                column: "CreatedByUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanDePaiements_Users_CreatedByUserID",
                table: "PlanDePaiements");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanDePaiements_Users_CreatedByUserID",
                table: "PlanDePaiements",
                column: "CreatedByUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
