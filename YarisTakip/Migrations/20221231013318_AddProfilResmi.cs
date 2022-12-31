using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YarisTakip.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilResmi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilResimUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilResimUrl",
                table: "AspNetUsers");
        }
    }
}
