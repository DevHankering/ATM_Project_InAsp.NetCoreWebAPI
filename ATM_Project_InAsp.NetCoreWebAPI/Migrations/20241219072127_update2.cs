using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_Project_InAsp.NetCoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdhaarCardUrl",
                table: "Documents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PanCardUrl",
                table: "Documents",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdhaarCardUrl",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "PanCardUrl",
                table: "Documents");
        }
    }
}
