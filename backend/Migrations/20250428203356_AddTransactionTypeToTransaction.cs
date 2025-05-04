// ╔═══════════════════════════════════════════════════════════════════════════╗
// ║             💰 Migration: Add TransactionType to Transactions             
// ║  Adds a new column to indicate whether it's a BUY or SELL transaction.    
// ╚═══════════════════════════════════════════════════════════════════════════╝

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockAdvisorBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionTypeToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ========== Add 'TransactionType' column (e.g., "buy"/"sell") ========== //
            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ========== Remove 'TransactionType' column ========== //
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transactions");
        }
    }
}
