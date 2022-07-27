using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class addGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Group_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Teacher_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Group_Id);
                    table.ForeignKey(
                        name: "FK_Group_Teacher_Teacher_Id",
                        column: x => x.Teacher_Id,
                        principalTable: "Teacher",
                        principalColumn: "Teacher_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Group_Teacher_Id",
                table: "Group",
                column: "Teacher_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Group");
        }
    }
}
