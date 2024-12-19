using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_Project_InAsp.NetCoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "CreatedAtUniversalTimeZone");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtLocalTimeZone",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAtLocalTimeZone",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUniversalTimeZone",
                table: "Users",
                newName: "CreatedAt");
        }
    }
}
