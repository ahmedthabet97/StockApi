using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApi.Migrations
{
    /// <inheritdoc />
    public partial class addRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_OrderId1",
                table: "Stocks",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Orders_OrderId1",
                table: "Stocks",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Orders_OrderId1",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_OrderId1",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Stocks");
        }
    }
}
