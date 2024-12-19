using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_Project_InAsp.NetCoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Documents_DocumentDomainId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "DocumentDomainId",
                table: "Users",
                newName: "DocumentsId");

            migrationBuilder.RenameColumn(
                name: "DocumentAddress",
                table: "Users",
                newName: "DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DocumentDomainId",
                table: "Users",
                newName: "IX_Users_DocumentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Documents_DocumentsId",
                table: "Users",
                column: "DocumentsId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Documents_DocumentsId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "DocumentsId",
                table: "Users",
                newName: "DocumentDomainId");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "Users",
                newName: "DocumentAddress");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DocumentsId",
                table: "Users",
                newName: "IX_Users_DocumentDomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Documents_DocumentDomainId",
                table: "Users",
                column: "DocumentDomainId",
                principalTable: "Documents",
                principalColumn: "Id");
        }
    }
}
