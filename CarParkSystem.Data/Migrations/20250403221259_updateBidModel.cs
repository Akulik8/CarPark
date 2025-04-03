using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateBidModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Bids");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Bids",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
