using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShoppingCardUI.Migrations
{
    /// <inheritdoc />
    public partial class AddStockEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "orderStatuses");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "orderStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
