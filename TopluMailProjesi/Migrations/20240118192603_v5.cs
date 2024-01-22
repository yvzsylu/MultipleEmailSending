using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopluMailProjesi.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmtpAyarlaris",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SunucuAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmtpAyarlaris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmtpAyarlaris_Kullanici_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanici",
                        principalColumn: "KullaniciID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmtpAyarlaris_KullaniciId",
                table: "SmtpAyarlaris",
                column: "KullaniciId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmtpAyarlaris");
        }
    }
}
