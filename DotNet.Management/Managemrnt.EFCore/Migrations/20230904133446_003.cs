using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managemrnt.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class _003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResponseContentType",
                table: "AuditLog",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDateTime",
                table: "AuditLog",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseDateTime",
                table: "AuditLog");

            migrationBuilder.UpdateData(
                table: "AuditLog",
                keyColumn: "ResponseContentType",
                keyValue: null,
                column: "ResponseContentType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ResponseContentType",
                table: "AuditLog",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
