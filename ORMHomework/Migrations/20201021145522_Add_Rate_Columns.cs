using Microsoft.EntityFrameworkCore.Migrations;

namespace ORMHomework.Migrations
{
    public partial class Add_Rate_Columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "tbl_Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "tbl_Faculties",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "tbl_Groups");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "tbl_Faculties");
        }
    }
}
