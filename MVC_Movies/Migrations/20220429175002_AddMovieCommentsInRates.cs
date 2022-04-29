using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_Movies.Migrations
{
    public partial class AddMovieCommentsInRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "MovieRate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "MovieRate");
        }
    }
}
