using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandsToDbAndSeedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Toyota" },
                    { 2, 2, "Subaru" },
                    { 3, 3, "BMW" },
                    { 4, 4, "Mercedes" },
                    { 5, 5, "Mitsubishi" },
                    { 6, 6, "Nissan" },
                    { 7, 7, "Opel" },
                    { 8, 8, "Renault" },
                    { 9, 9, "Honda" },
                    { 10, 10, "Audi" },
                    { 11, 11, "Volvo" },
                    { 12, 12, "Kia" },
                    { 13, 13, "Hyundai" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
