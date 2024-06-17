using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReadersRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reader_role",
                columns: table => new
                {
                    readers_id = table.Column<Guid>(type: "uuid", nullable: false),
                    roles_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reader_role", x => new { x.readers_id, x.roles_id });
                    table.ForeignKey(
                        name: "fk_reader_role_readers_readers_id",
                        column: x => x.readers_id,
                        principalTable: "readers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reader_role_role_roles_id",
                        column: x => x.roles_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("0a1a389c-2b6a-411e-a969-3fa836079efc"), "Admin" },
                    { new Guid("a314f90a-291d-45ff-827b-17ce4fd4dfff"), "Reader" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_reader_role_roles_id",
                table: "reader_role",
                column: "roles_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_id",
                table: "roles",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reader_role");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
