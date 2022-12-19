using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTNETExamenproject.Migrations
{
    /// <inheritdoc />
    public partial class modelModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Klant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Bedrijf",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Klant");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Bedrijf");
        }
    }
}
