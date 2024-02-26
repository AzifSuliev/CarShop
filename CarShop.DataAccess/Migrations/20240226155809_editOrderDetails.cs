using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editOrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderDetails",
                newName: "FullEquipmentPrice");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "OrderDetails",
                newName: "CountFull");

            migrationBuilder.AddColumn<double>(
                name: "BasicEquipmentPrice",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "CountBasic",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicEquipmentPrice",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CountBasic",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "FullEquipmentPrice",
                table: "OrderDetails",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "CountFull",
                table: "OrderDetails",
                newName: "Count");
        }
    }
}
