using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountColm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "discounts",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discounts",
                table: "Order");
        }
    }
}
