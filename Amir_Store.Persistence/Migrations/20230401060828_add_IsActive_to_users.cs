using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Amir_Store.Persistence.Migrations
{
    public partial class add_IsActive_to_users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 4, 1, 9, 38, 27, 583, DateTimeKind.Local).AddTicks(2912));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 4, 1, 9, 38, 27, 584, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2023, 4, 1, 9, 38, 27, 584, DateTimeKind.Local).AddTicks(6512));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 3, 29, 11, 59, 36, 803, DateTimeKind.Local).AddTicks(4018));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 3, 29, 11, 59, 36, 804, DateTimeKind.Local).AddTicks(7325));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2023, 3, 29, 11, 59, 36, 804, DateTimeKind.Local).AddTicks(7404));
        }
    }
}
