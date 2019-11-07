using Microsoft.EntityFrameworkCore.Migrations;

namespace PabedaSchool.Migrations
{
    public partial class FixSchoolTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolSet_TeacherSet_TeacherId",
                table: "SchoolSet");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSchool_SchoolSet_SchoolId",
                table: "TeacherSchool");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSchool_TeacherSet_TeacherId",
                table: "TeacherSchool");

            migrationBuilder.DropIndex(
                name: "IX_SchoolSet_TeacherId",
                table: "SchoolSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSchool",
                table: "TeacherSchool");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SchoolSet");

            migrationBuilder.RenameTable(
                name: "TeacherSchool",
                newName: "TeacherSchoolSet");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSchool_TeacherId",
                table: "TeacherSchoolSet",
                newName: "IX_TeacherSchoolSet_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSchool_SchoolId",
                table: "TeacherSchoolSet",
                newName: "IX_TeacherSchoolSet_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSchoolSet",
                table: "TeacherSchoolSet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSchoolSet_SchoolSet_SchoolId",
                table: "TeacherSchoolSet",
                column: "SchoolId",
                principalTable: "SchoolSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSchoolSet_TeacherSet_TeacherId",
                table: "TeacherSchoolSet",
                column: "TeacherId",
                principalTable: "TeacherSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSchoolSet_SchoolSet_SchoolId",
                table: "TeacherSchoolSet");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSchoolSet_TeacherSet_TeacherId",
                table: "TeacherSchoolSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSchoolSet",
                table: "TeacherSchoolSet");

            migrationBuilder.RenameTable(
                name: "TeacherSchoolSet",
                newName: "TeacherSchool");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSchoolSet_TeacherId",
                table: "TeacherSchool",
                newName: "IX_TeacherSchool_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSchoolSet_SchoolId",
                table: "TeacherSchool",
                newName: "IX_TeacherSchool_SchoolId");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "SchoolSet",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSchool",
                table: "TeacherSchool",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSet_TeacherId",
                table: "SchoolSet",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolSet_TeacherSet_TeacherId",
                table: "SchoolSet",
                column: "TeacherId",
                principalTable: "TeacherSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSchool_SchoolSet_SchoolId",
                table: "TeacherSchool",
                column: "SchoolId",
                principalTable: "SchoolSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSchool_TeacherSet_TeacherId",
                table: "TeacherSchool",
                column: "TeacherId",
                principalTable: "TeacherSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
