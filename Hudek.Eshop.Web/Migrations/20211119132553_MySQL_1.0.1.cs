using Microsoft.EntityFrameworkCore.Migrations;

namespace Hudek.Eshop.Web.Migrations
{
    public partial class MySQL_101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageSource600x700",
                table: "ProductItem",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSource600x700",
                table: "ProductItem");
        }
    }
}
