using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShop.Migrations
{
    /// <inheritdoc />
    public partial class EditCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Легковой фурон");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Легвой фурон");
        }
    }
}
