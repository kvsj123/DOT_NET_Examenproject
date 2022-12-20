using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTNETExamenproject.Migrations
{
    /// <inheritdoc />
    public partial class OfferteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offerte",
                columns: table => new
                {
                    OfferteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitelOfferte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotaalBedrag = table.Column<float>(type: "real", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    KlantId = table.Column<int>(type: "int", nullable: false),
                    BedrijfId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offerte", x => x.OfferteId);
                    table.ForeignKey(
                        name: "FK_Offerte_Bedrijf_BedrijfId",
                        column: x => x.BedrijfId,
                        principalTable: "Bedrijf",
                        principalColumn: "BedrijfId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offerte_Klant_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klant",
                        principalColumn: "KlantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offerte_BedrijfId",
                table: "Offerte",
                column: "BedrijfId");

            migrationBuilder.CreateIndex(
                name: "IX_Offerte_KlantId",
                table: "Offerte",
                column: "KlantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offerte");
        }
    }
}
