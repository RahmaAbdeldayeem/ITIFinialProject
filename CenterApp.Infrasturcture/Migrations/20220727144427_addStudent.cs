using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class addStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Student_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Student_Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Student_BirthOfDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Student_RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 7, 27, 16, 44, 27, 744, DateTimeKind.Local).AddTicks(9686)),
                    Student_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Student_Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Student_StdPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Student_ParentPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stage_id = table.Column<int>(type: "int", nullable: false),
                    Group_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Student_Id);
                    table.ForeignKey(
                        name: "FK_Students_Group_Group_id",
                        column: x => x.Group_id,
                        principalTable: "Group",
                        principalColumn: "Group_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Stages_Stage_id",
                        column: x => x.Stage_id,
                        principalTable: "Stages",
                        principalColumn: "Stage_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Group_id",
                table: "Students",
                column: "Group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Stage_id",
                table: "Students",
                column: "Stage_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
