using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class addlevelMatrial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Stage_Name",
                table: "Stages",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Level_Name",
                table: "Levels",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Matrials",
                columns: table => new
                {
                    Matrial_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matrial_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matrials", x => x.Matrial_Id);
                });

            migrationBuilder.CreateTable(
                name: "LevelMatrial",
                columns: table => new
                {
                    Level_Id = table.Column<int>(type: "int", nullable: false),
                    Matrial_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelMatrial", x => new { x.Level_Id, x.Matrial_Id });
                    table.ForeignKey(
                        name: "FK_LevelMatrial_Levels_Level_Id",
                        column: x => x.Level_Id,
                        principalTable: "Levels",
                        principalColumn: "Level_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LevelMatrial_Matrials_Matrial_Id",
                        column: x => x.Matrial_Id,
                        principalTable: "Matrials",
                        principalColumn: "Matrial_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LevelMatrial_Matrial_Id",
                table: "LevelMatrial",
                column: "Matrial_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LevelMatrial");

            migrationBuilder.DropTable(
                name: "Matrials");

            migrationBuilder.AlterColumn<string>(
                name: "Stage_Name",
                table: "Stages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Level_Name",
                table: "Levels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
