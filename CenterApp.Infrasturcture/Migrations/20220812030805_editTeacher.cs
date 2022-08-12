using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class editTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Student_RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 5, 8, 5, 553, DateTimeKind.Local).AddTicks(7482),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 27, 18, 21, 39, 992, DateTimeKind.Local).AddTicks(1931));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Student_RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 27, 18, 21, 39, 992, DateTimeKind.Local).AddTicks(1931),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 5, 8, 5, 553, DateTimeKind.Local).AddTicks(7482));
        }
    }
}
