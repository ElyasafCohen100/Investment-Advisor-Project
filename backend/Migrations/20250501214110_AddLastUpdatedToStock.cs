// ╔══════════════════════════════════════════════════════════════════════╗
// ║      🕒 Migration: Add 'LastUpdated' Column to Stocks Table              
// ║  Adds a datetime column to track the last update of stock price.        
// ╚══════════════════════════════════════════════════════════════════════╝


using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockAdvisorBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddLastUpdatedToStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ========= Add 'LastUpdated' column to 'Stocks' table ========== //
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Stocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ========= Remove 'LastUpdated' column from 'Stocks' table ========== //
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Stocks");
        }
    }
}
