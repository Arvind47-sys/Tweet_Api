using Microsoft.EntityFrameworkCore.Migrations;

namespace Tweet_Api.Data.Migrations
{
    public partial class AddedTagForReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Replies");
        }
    }
}
