using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockAdvisorBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddEventsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioItems_Users_UserId",
                table: "PortfolioItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioItems_UserId",
                table: "PortfolioItems");

            migrationBuilder.DropColumn(
                name: "IsPurchase",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Transactions",
                newName: "TransactionAmount");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "PortfolioItems",
                newName: "PortfolioQuantity");

            migrationBuilder.RenameColumn(
                name: "PurchasePrice",
                table: "PortfolioItems",
                newName: "AveragePurchasePrice");

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "PortfolioItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AggregateType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AggregateId = table.Column<int>(type: "int", nullable: false),
                    EventData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserModelId",
                table: "Transactions",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItems_UserModelId",
                table: "PortfolioItems",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioItems_Users_UserModelId",
                table: "PortfolioItems",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserModelId",
                table: "Transactions",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioItems_Users_UserModelId",
                table: "PortfolioItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserModelId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserModelId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioItems_UserModelId",
                table: "PortfolioItems");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "PortfolioItems");

            migrationBuilder.RenameColumn(
                name: "TransactionAmount",
                table: "Transactions",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "PortfolioQuantity",
                table: "PortfolioItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "AveragePurchasePrice",
                table: "PortfolioItems",
                newName: "PurchasePrice");

            migrationBuilder.AddColumn<bool>(
                name: "IsPurchase",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItems_UserId",
                table: "PortfolioItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioItems_Users_UserId",
                table: "PortfolioItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
