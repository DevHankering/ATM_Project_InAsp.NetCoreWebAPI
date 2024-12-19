using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_Project_InAsp.NetCoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Documents_DocumentsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DocumentsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DocumentsId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DocumentId",
                table: "Users",
                column: "DocumentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Documents_DocumentId",
                table: "Users",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Documents_DocumentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DocumentId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentsId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DocumentsId",
                table: "Users",
                column: "DocumentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Documents_DocumentsId",
                table: "Users",
                column: "DocumentsId",
                principalTable: "Documents",
                principalColumn: "Id");
        }
    }
}
