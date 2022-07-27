using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class addTeacherMatrial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Teacher_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Teacher_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teacher_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teacher_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teacher_Specilist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teacher_Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teacher_BirthOfDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Teacher_Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherMatrial",
                columns: table => new
                {
                    Matrial_Id = table.Column<int>(type: "int", nullable: false),
                    Teacher_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherMatrial", x => new { x.Matrial_Id, x.Teacher_Id });
                    table.ForeignKey(
                        name: "FK_TeacherMatrial_Matrials_Matrial_Id",
                        column: x => x.Matrial_Id,
                        principalTable: "Matrials",
                        principalColumn: "Matrial_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherMatrial_Teacher_Teacher_Id",
                        column: x => x.Teacher_Id,
                        principalTable: "Teacher",
                        principalColumn: "Teacher_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherMatrial_Teacher_Id",
                table: "TeacherMatrial",
                column: "Teacher_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherMatrial");

            migrationBuilder.DropTable(
                name: "Teacher");
        }
    }
}
