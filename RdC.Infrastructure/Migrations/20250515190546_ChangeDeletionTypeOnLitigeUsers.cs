using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeletionTypeOnLitigeUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Litiges_Users_DeclaredByUserID",
                table: "Litiges");

            migrationBuilder.AddForeignKey(
                name: "FK_Litiges_Users_DeclaredByUserID",
                table: "Litiges",
                column: "DeclaredByUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Litiges_Users_DeclaredByUserID",
                table: "Litiges");

            migrationBuilder.AddForeignKey(
                name: "FK_Litiges_Users_DeclaredByUserID",
                table: "Litiges",
                column: "DeclaredByUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
