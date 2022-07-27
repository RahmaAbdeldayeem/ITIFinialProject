using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class addstageidINTeacherMatrial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stage_Id",
                table: "TeacherMatrial",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherMatrial_Stage_Id",
                table: "TeacherMatrial",
                column: "Stage_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherMatrial_Stages_Stage_Id",
                table: "TeacherMatrial",
                column: "Stage_Id",
                principalTable: "Stages",
                principalColumn: "Stage_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherMatrial_Stages_Stage_Id",
                table: "TeacherMatrial");

            migrationBuilder.DropIndex(
                name: "IX_TeacherMatrial_Stage_Id",
                table: "TeacherMatrial");

            migrationBuilder.DropColumn(
                name: "Stage_Id",
                table: "TeacherMatrial");
        }
    }
}
