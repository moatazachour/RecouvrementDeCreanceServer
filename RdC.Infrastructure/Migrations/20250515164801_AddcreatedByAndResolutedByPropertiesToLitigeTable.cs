using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddcreatedByAndResolutedByPropertiesToLitigeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeclaredByUserID",
                table: "Litiges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResolutedByUserID",
                table: "Litiges",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Litiges_DeclaredByUserID",
                table: "Litiges",
                column: "DeclaredByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Litiges_Users_DeclaredByUserID",
                table: "Litiges",
                column: "DeclaredByUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Litiges_Users_DeclaredByUserID",
                table: "Litiges");

            migrationBuilder.DropIndex(
                name: "IX_Litiges_DeclaredByUserID",
                table: "Litiges");

            migrationBuilder.DropColumn(
                name: "DeclaredByUserID",
                table: "Litiges");

            migrationBuilder.DropColumn(
                name: "ResolutedByUserID",
                table: "Litiges");
        }
    }
}
