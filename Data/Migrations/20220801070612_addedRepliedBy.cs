using Microsoft.EntityFrameworkCore.Migrations;

namespace Tweet_Api.Data.Migrations
{
    public partial class addedRepliedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RepliedBy",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepliedBy",
                table: "Replies");
        }
    }
}
