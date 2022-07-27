using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class addlevelandStage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Level_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Level_Id);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Stage_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stage_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Stage_Id);
                    table.ForeignKey(
                        name: "FK_Stages_Levels_Level_Id",
                        column: x => x.Level_Id,
                        principalTable: "Levels",
                        principalColumn: "Level_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stages_Level_Id",
                table: "Stages",
                column: "Level_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Levels");
        }
    }
}
