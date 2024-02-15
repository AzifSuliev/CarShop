using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editShoppingCartModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "ShoppingCarts",
                newName: "CountFull");

            migrationBuilder.AddColumn<int>(
                name: "CountBasic",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountBasic",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "CountFull",
                table: "ShoppingCarts",
                newName: "Count");
        }
    }
}
