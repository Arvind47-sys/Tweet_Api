using Microsoft.EntityFrameworkCore.Migrations;

namespace Tweet_Api.Data.Migrations
{
    public partial class addedUserNameToTweet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Tweets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Tweets");
        }
    }
}
