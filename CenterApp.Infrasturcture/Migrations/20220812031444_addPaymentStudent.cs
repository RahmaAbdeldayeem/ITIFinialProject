using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class addPaymentStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Student_RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 5, 14, 44, 657, DateTimeKind.Local).AddTicks(8817),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 5, 8, 5, 553, DateTimeKind.Local).AddTicks(7482));

            migrationBuilder.CreateTable(
                name: "StudentPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_Id = table.Column<int>(type: "int", nullable: false),
                    Matrial_Id = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentPayments_Matrials_Matrial_Id",
                        column: x => x.Matrial_Id,
                        principalTable: "Matrials",
                        principalColumn: "Matrial_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentPayments_Students_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Students",
                        principalColumn: "Student_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentPayments_Matrial_Id",
                table: "StudentPayments",
                column: "Matrial_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPayments_Student_Id",
                table: "StudentPayments",
                column: "Student_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentPayments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Student_RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 5, 8, 5, 553, DateTimeKind.Local).AddTicks(7482),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 5, 14, 44, 657, DateTimeKind.Local).AddTicks(8817));
        }
    }
}
