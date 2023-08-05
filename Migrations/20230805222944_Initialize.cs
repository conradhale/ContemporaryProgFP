using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IT3045C_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Program = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Culture",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    BirthCountry = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FirstLanguage = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PrimaryLanguage = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Culture", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Culture_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    FavoriteStore = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FavoriteHobby = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FavoriteAnimal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FavoriteSeason = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Favorites_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opinions",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    IOSOverAdroid = table.Column<bool>(type: "bit", nullable: false),
                    PineappleOnPizza = table.Column<bool>(type: "bit", nullable: false),
                    CatsOverDogs = table.Column<bool>(type: "bit", nullable: false),
                    PlaystationOverXbox = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinions", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Opinions_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Culture");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Opinions");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
