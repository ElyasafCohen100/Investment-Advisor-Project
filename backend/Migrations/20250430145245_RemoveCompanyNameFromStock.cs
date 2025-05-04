// ╔══════════════════════════════════════════════════════════════════════════╗
// ║       🧹 Migration: Remove CompanyName from Stock Table                  
// ║  This migration deletes the CompanyName column from the Stocks table.    
// ║  Can be restored in the Down method if rollback is needed.               
// ╚══════════════════════════════════════════════════════════════════════════╝

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockAdvisorBackend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCompanyNameFromStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ========= Remove 'CompanyName' column from 'Stocks' ========== //
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Stocks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ========= Add 'CompanyName' column back to 'Stocks' ========== //
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
