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
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Stocks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
