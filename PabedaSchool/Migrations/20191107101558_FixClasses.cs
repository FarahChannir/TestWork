using Microsoft.EntityFrameworkCore.Migrations;

namespace PabedaSchool.Migrations
{
    public partial class FixClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchool_SchoolSet_SchoolId",
                table: "ClassSchool");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchool_StudentSet_StudentId",
                table: "ClassSchool");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchool_TeacherSet_TeacherId",
                table: "ClassSchool");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassSchool",
                table: "ClassSchool");

            migrationBuilder.RenameTable(
                name: "ClassSchool",
                newName: "ClassSchoolSet");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSchool_TeacherId",
                table: "ClassSchoolSet",
                newName: "IX_ClassSchoolSet_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSchool_StudentId",
                table: "ClassSchoolSet",
                newName: "IX_ClassSchoolSet_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSchool_SchoolId",
                table: "ClassSchoolSet",
                newName: "IX_ClassSchoolSet_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassSchoolSet",
                table: "ClassSchoolSet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchoolSet_SchoolSet_SchoolId",
                table: "ClassSchoolSet",
                column: "SchoolId",
                principalTable: "SchoolSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchoolSet_StudentSet_StudentId",
                table: "ClassSchoolSet",
                column: "StudentId",
                principalTable: "StudentSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchoolSet_TeacherSet_TeacherId",
                table: "ClassSchoolSet",
                column: "TeacherId",
                principalTable: "TeacherSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchoolSet_SchoolSet_SchoolId",
                table: "ClassSchoolSet");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchoolSet_StudentSet_StudentId",
                table: "ClassSchoolSet");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchoolSet_TeacherSet_TeacherId",
                table: "ClassSchoolSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassSchoolSet",
                table: "ClassSchoolSet");

            migrationBuilder.RenameTable(
                name: "ClassSchoolSet",
                newName: "ClassSchool");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSchoolSet_TeacherId",
                table: "ClassSchool",
                newName: "IX_ClassSchool_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSchoolSet_StudentId",
                table: "ClassSchool",
                newName: "IX_ClassSchool_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSchoolSet_SchoolId",
                table: "ClassSchool",
                newName: "IX_ClassSchool_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassSchool",
                table: "ClassSchool",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchool_SchoolSet_SchoolId",
                table: "ClassSchool",
                column: "SchoolId",
                principalTable: "SchoolSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchool_StudentSet_StudentId",
                table: "ClassSchool",
                column: "StudentId",
                principalTable: "StudentSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchool_TeacherSet_TeacherId",
                table: "ClassSchool",
                column: "TeacherId",
                principalTable: "TeacherSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
