using Microsoft.EntityFrameworkCore.Migrations;

namespace ORMHomework.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(maxLength: 15, nullable: false),
                    FacultyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Groups_tbl_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "tbl_Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 15, nullable: false),
                    LastName = table.Column<string>(maxLength: 15, nullable: false),
                    Age = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Students_tbl_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "tbl_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Groups_FacultyId",
                table: "tbl_Groups",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Students_GroupId",
                table: "tbl_Students",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Students");

            migrationBuilder.DropTable(
                name: "tbl_Groups");

            migrationBuilder.DropTable(
                name: "tbl_Faculties");
        }
    }
}
