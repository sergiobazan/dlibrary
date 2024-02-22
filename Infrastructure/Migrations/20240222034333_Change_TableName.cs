using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Change_TableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reader",
                table: "Reader");

            migrationBuilder.RenameTable(
                name: "Reader",
                newName: "readers");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "readers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "readers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "FullName_Name",
                table: "readers",
                newName: "full_name_name");

            migrationBuilder.RenameColumn(
                name: "FullName_LastName",
                table: "readers",
                newName: "full_name_last_name");

            migrationBuilder.RenameIndex(
                name: "IX_Reader_Email",
                table: "readers",
                newName: "ix_readers_email");

            migrationBuilder.AddPrimaryKey(
                name: "pk_readers",
                table: "readers",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_readers",
                table: "readers");

            migrationBuilder.RenameTable(
                name: "readers",
                newName: "Reader");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Reader",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Reader",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "full_name_name",
                table: "Reader",
                newName: "FullName_Name");

            migrationBuilder.RenameColumn(
                name: "full_name_last_name",
                table: "Reader",
                newName: "FullName_LastName");

            migrationBuilder.RenameIndex(
                name: "ix_readers_email",
                table: "Reader",
                newName: "IX_Reader_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reader",
                table: "Reader",
                column: "Id");
        }
    }
}
