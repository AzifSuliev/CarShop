using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarShop_RazorTemp.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Седан" },
                    { 2, 2, "Купе" },
                    { 3, 3, "Хэтчбек" },
                    { 4, 4, "Лифтбек" },
                    { 5, 5, "Фастбек" },
                    { 6, 6, "Универсал" },
                    { 7, 7, "Кроссовер" },
                    { 8, 8, "Внедорожник" },
                    { 9, 9, "Пикап" },
                    { 10, 10, "Легковой фургон" },
                    { 11, 11, "Минивен" },
                    { 12, 12, "Компактвен" },
                    { 13, 13, "Микровен" },
                    { 14, 14, "Кабриолет" },
                    { 15, 15, "Родстер" },
                    { 16, 16, "Тарга" },
                    { 17, 17, "Ландо" },
                    { 18, 18, "Лимузин" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
