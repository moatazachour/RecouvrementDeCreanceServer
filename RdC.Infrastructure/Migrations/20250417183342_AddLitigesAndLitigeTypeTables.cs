using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLitigesAndLitigeTypeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LitigeTypes",
                columns: table => new
                {
                    LitigeTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    TypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LitigeTypes", x => x.LitigeTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Litiges",
                columns: table => new
                {
                    LitigeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactureID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    LitigeStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    LitigeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Litiges", x => x.LitigeID);
                    table.ForeignKey(
                        name: "FK_Litiges_Factures_FactureID",
                        column: x => x.FactureID,
                        principalTable: "Factures",
                        principalColumn: "FactureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Litiges_LitigeTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "LitigeTypes",
                        principalColumn: "LitigeTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Litiges_FactureID",
                table: "Litiges",
                column: "FactureID");

            migrationBuilder.CreateIndex(
                name: "IX_Litiges_TypeID",
                table: "Litiges",
                column: "TypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Litiges");

            migrationBuilder.DropTable(
                name: "LitigeTypes");
        }
    }
}
