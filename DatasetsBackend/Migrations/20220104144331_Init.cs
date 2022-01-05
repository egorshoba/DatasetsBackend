using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatasetsBackend.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Datasets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContainsCyrillic = table.Column<bool>(type: "bit", nullable: false),
                    ContainsLatin = table.Column<bool>(type: "bit", nullable: false),
                    ContainsDigits = table.Column<bool>(type: "bit", nullable: false),
                    ContainsSpecChars = table.Column<bool>(type: "bit", nullable: false),
                    CaseSensitive = table.Column<bool>(type: "bit", nullable: false),
                    AnswersLocation = table.Column<int>(type: "int", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datasets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Datasets");
        }
    }
}
