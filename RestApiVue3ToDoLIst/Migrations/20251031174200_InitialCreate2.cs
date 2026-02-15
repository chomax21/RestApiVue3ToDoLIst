using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApiVue3ToDoLIst.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Jobs",
                type: "TIMESTAMP",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Jobs",
                type: "TIMESTAMP",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Jobs",
                type: "TIMESTAMP",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Jobs",
                type: "TIMESTAMP",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP",
                oldNullable: true);
        }
    }
}
