// ╔═══════════════════════════════════════════════════════════════════════════╗
// ║                🛠️ Migration: Make Response & Question Nullable            
// ║  This migration makes the Response and Question columns optional in DB.  
// ╚═══════════════════════════════════════════════════════════════════════════╝

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockAdvisorBackend.Migrations
{
    /// <inheritdoc />
    public partial class MakeResponseNullableInAdviceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ========== Make 'Response' column nullable ========== //
            migrationBuilder.AlterColumn<string>(
                name: "Response",
                table: "AdviceRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // ========== Make 'Question' column nullable ========== //
            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "AdviceRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ========== Revert 'Response' column to NOT NULL ========== //
            migrationBuilder.AlterColumn<string>(
                name: "Response",
                table: "AdviceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            // ========== Revert 'Question' column to NOT NULL ========== //
            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "AdviceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
