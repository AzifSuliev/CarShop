using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editOrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SreetAddress",
                table: "OrderHeaders",
                newName: "StreetAddress");

            migrationBuilder.RenameColumn(
                name: "Nane",
                table: "OrderHeaders",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "OrderHeaders",
                newName: "SreetAddress");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "OrderHeaders",
                newName: "Nane");
        }
    }
}
