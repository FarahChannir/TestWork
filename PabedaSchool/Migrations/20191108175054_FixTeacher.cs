using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PabedaSchool.Migrations
{
    public partial class FixTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogSystemSet");

            migrationBuilder.RenameColumn(
                name: "PassportNomber",
                table: "TeacherSet",
                newName: "Passport_Nomber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Passport_Nomber",
                table: "TeacherSet",
                newName: "PassportNomber");

            migrationBuilder.CreateTable(
                name: "LogSystemSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Scource = table.Column<string>(nullable: true),
                    TextExption = table.Column<string>(nullable: true),
                    TextIneerExption = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogSystemSet", x => x.Id);
                });
        }
    }
}
