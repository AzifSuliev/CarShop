using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProductToDbAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    basicEquipmentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fullEquipmentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CarName", "Description", "basicEquipmentPrice", "fullEquipmentPrice" },
                values: new object[,]
                {
                    { 1, "BMW 318i", "Мощность двигателя: 156 л.с\nРасход топлива: 6,3 л/100 км", 120000m, 2000000m },
                    { 2, "BMW X6 M50i", "Мощность двигателя: 480 л.с.\nРасход топлива в л/100км (смешанный цикл): 11.5", 1000000m, 180000m },
                    { 3, "Mercedes-Benz C118/X118", "Мощность двигателя: 421 л.с.\nРасход топлива: 9.2 на 100 км", 1500000m, 2000000m },
                    { 4, "Nissan Tiida", "Мощность двигателя: 128 л.с.\nРасход топлива: 4.7-10.1 л/100 км", 700000m, 850000m },
                    { 5, "Toyota Sequoia", "Мощность двигателя: 437 л.с.n\nРасход топлива: 11.7-23 л/100 км", 1200000m, 1400000m },
                    { 6, "Kia XCeed", "Мощность двигателя: 200 л.с.n\nРасход топлива: 7.1 л/100 км", 600000m, 750000m },
                    { 7, "Honda Civic 1.0", "Мощность двигателя: 129 л.с.n\nРасход топлива: 5.5 л/100 км", 600000m, 750000m },
                    { 8, "HYUNDAI Tucson", "Мощность двигателя: 185 л.с.n\nРасход топлива: 5.4 л/100 км", 600000m, 750000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
