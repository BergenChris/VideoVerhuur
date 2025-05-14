using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoVerhuur.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenraNaam",
                table: "Genres",
                newName: "GenreNaam");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenreNaam",
                table: "Genres",
                newName: "GenraNaam");
        }
    }
}
