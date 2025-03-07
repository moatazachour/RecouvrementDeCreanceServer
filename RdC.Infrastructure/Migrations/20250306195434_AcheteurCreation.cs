using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RdC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AcheteurCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acheteurs",
                columns: table => new
                {
                    AcheteurID = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acheteurs", x => x.AcheteurID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acheteurs");
        }
    }
}
