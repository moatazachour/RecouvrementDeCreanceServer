using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddJustificationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Justificatifs",
                columns: table => new
                {
                    JustificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LitigeID = table.Column<int>(type: "int", nullable: false),
                    NomFicher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheminFichier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAjout = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Justificatifs", x => x.JustificationID);
                    table.ForeignKey(
                        name: "FK_Justificatifs_Litiges_LitigeID",
                        column: x => x.LitigeID,
                        principalTable: "Litiges",
                        principalColumn: "LitigeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Justificatifs_LitigeID",
                table: "Justificatifs",
                column: "LitigeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Justificatifs");
        }
    }
}
