using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YarisTakip.Migrations
{
    /// <inheritdoc />
    public partial class AddKullaniciYaris : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KullaniciYarisi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    YarisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciYarisi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KullaniciYarisi_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KullaniciYarisi_Yarislar_YarisId",
                        column: x => x.YarisId,
                        principalTable: "Yarislar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciYarisi_KullaniciId",
                table: "KullaniciYarisi",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciYarisi_YarisId",
                table: "KullaniciYarisi",
                column: "YarisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciYarisi");
        }
    }
}
