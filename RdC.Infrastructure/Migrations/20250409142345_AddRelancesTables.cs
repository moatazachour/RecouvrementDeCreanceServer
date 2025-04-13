using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelancesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relances",
                columns: table => new
                {
                    RelanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaiementDateID = table.Column<int>(type: "int", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    RelanceType = table.Column<int>(type: "int", nullable: false),
                    DateDeEnvoie = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relances", x => x.RelanceID);
                    table.ForeignKey(
                        name: "FK_Relances_PaiementDates_PaiementDateID",
                        column: x => x.PaiementDateID,
                        principalTable: "PaiementDates",
                        principalColumn: "DateEcheanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailRelances",
                columns: table => new
                {
                    RelanceID = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailRelances", x => x.RelanceID);
                    table.ForeignKey(
                        name: "FK_EmailRelances_Relances_RelanceID",
                        column: x => x.RelanceID,
                        principalTable: "Relances",
                        principalColumn: "RelanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SMSRelances",
                columns: table => new
                {
                    RelanceID = table.Column<int>(type: "int", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SMSBody = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSRelances", x => x.RelanceID);
                    table.ForeignKey(
                        name: "FK_SMSRelances_Relances_RelanceID",
                        column: x => x.RelanceID,
                        principalTable: "Relances",
                        principalColumn: "RelanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relances_PaiementDateID",
                table: "Relances",
                column: "PaiementDateID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailRelances");

            migrationBuilder.DropTable(
                name: "SMSRelances");

            migrationBuilder.DropTable(
                name: "Relances");
        }
    }
}
