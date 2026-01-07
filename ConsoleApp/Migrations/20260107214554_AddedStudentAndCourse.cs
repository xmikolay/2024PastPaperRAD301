using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedStudentAndCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "ID", "CourseName", "Description", "LecturerID", "QQILevel" },
                values: new object[] { 3, "Functional Programming", "blah blah for loop", 2, 6 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "ID", "Age", "Eircode", "Email", "FirstName", "Surname" },
                values: new object[] { 4, 22, "A84 FP91", "jacky@yahoo.com", "Jack", "O'Hagan" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
