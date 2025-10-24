using System;
using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestApiVue3ToDoLIst.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    StatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusName = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false),
                    Name = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false),
                    SurName = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false),
                    LastName = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false),
                    Position = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false),
                    Password = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false),
                    Description = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignedToId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_Users_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusCode", "StatusName" },
                values: new object[,]
                {
                    { 1, 1, "ToDo" },
                    { 2, 2, "InProgress" },
                    { 3, 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "LastName", "Login", "Name", "Password", "Position", "SurName" },
                values: new object[] { 1, "_", "Admin", "Admin", "Admin", "boss", "_" });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_AssignedToId",
                table: "Jobs",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CreatedById",
                table: "Jobs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_StatusId",
                table: "Jobs",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
