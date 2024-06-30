using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpRegisterWithImgApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Occupation",
                table: "empModels",
                newName: "EmployeeOccupation");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "empModels",
                newName: "EmployeeName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "empModels",
                newName: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeOccupation",
                table: "empModels",
                newName: "Occupation");

            migrationBuilder.RenameColumn(
                name: "EmployeeName",
                table: "empModels",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "empModels",
                newName: "Id");
        }
    }
}
