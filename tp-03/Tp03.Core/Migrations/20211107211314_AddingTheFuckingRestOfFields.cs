using Microsoft.EntityFrameworkCore.Migrations;

namespace Tp03.Core.Migrations
{
    public partial class AddingTheFuckingRestOfFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Consignee",
                table: "BillsOfLading",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consignee",
                table: "BillsOfLading");
        }
    }
}
