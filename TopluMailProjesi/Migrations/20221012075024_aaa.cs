using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopluMailProjesi.Migrations
{
    public partial class aaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanici",
                columns: table => new
                {
                    KullaniciID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Adi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IslemSayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanici", x => x.KullaniciID);
                });

            migrationBuilder.CreateTable(
                name: "Dokumanlar",
                columns: table => new
                {
                    DokumanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    DokumanYolu = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    KullaniciId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dokumanl__53AEF7B0DBC12E54", x => x.DokumanID);
                    table.ForeignKey(
                        name: "FK_Dokumanlar_Kullanici_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanici",
                        principalColumn: "KullaniciID");
                });

            migrationBuilder.CreateTable(
                name: "Grup",
                columns: table => new
                {
                    GrupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    GrupAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KullaniciID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grup", x => x.GrupID);
                    table.ForeignKey(
                        name: "FK__Grup__KullaniciI__2F10007B",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanici",
                        principalColumn: "KullaniciID");
                });

            migrationBuilder.CreateTable(
                name: "Kisi",
                columns: table => new
                {
                    KisiID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Adi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    KullaniciID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kisi", x => x.KisiID);
                    table.ForeignKey(
                        name: "FK__Kisi__KullaniciI__2B3F6F97",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanici",
                        principalColumn: "KullaniciID");
                });

            migrationBuilder.CreateTable(
                name: "MailTaslak",
                columns: table => new
                {
                    MailTaslakID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Baslik = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Aciklama = table.Column<string>(type: "ntext", nullable: false),
                    GonderimTarihi = table.Column<DateTime>(type: "datetime", nullable: false),
                    SorguNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KullaniciID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTaslak", x => x.MailTaslakID);
                    table.ForeignKey(
                        name: "FK__MailTasla__Kulla__38996AB5",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanici",
                        principalColumn: "KullaniciID");
                });

            migrationBuilder.CreateTable(
                name: "KisiGrup",
                columns: table => new
                {
                    KisiGrupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    KisiID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GrupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KullaniciID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KisiGrup", x => x.KisiGrupID);
                    table.ForeignKey(
                        name: "FK__KisiGrup__GrupID__33D4B598",
                        column: x => x.GrupID,
                        principalTable: "Grup",
                        principalColumn: "GrupID");
                    table.ForeignKey(
                        name: "FK__KisiGrup__KisiID__32E0915F",
                        column: x => x.KisiID,
                        principalTable: "Kisi",
                        principalColumn: "KisiID");
                    table.ForeignKey(
                        name: "FK__KisiGrup__Kullan__34C8D9D1",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanici",
                        principalColumn: "KullaniciID");
                });

            migrationBuilder.CreateTable(
                name: "MailDokuman",
                columns: table => new
                {
                    MailDokumanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MailTaslakID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DokumanID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailDokuman", x => x.MailDokumanID);
                    table.ForeignKey(
                        name: "FK__MailDokum__Dokum__3D5E1FD2",
                        column: x => x.DokumanID,
                        principalTable: "Dokumanlar",
                        principalColumn: "DokumanID");
                    table.ForeignKey(
                        name: "FK__MailDokum__MailT__3C69FB99",
                        column: x => x.MailTaslakID,
                        principalTable: "MailTaslak",
                        principalColumn: "MailTaslakID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dokumanlar_KullaniciId",
                table: "Dokumanlar",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Grup_KullaniciID",
                table: "Grup",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Kisi_KullaniciID",
                table: "Kisi",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_KisiGrup_GrupID",
                table: "KisiGrup",
                column: "GrupID");

            migrationBuilder.CreateIndex(
                name: "IX_KisiGrup_KisiID",
                table: "KisiGrup",
                column: "KisiID");

            migrationBuilder.CreateIndex(
                name: "IX_KisiGrup_KullaniciID",
                table: "KisiGrup",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_MailDokuman_DokumanID",
                table: "MailDokuman",
                column: "DokumanID");

            migrationBuilder.CreateIndex(
                name: "IX_MailDokuman_MailTaslakID",
                table: "MailDokuman",
                column: "MailTaslakID");

            migrationBuilder.CreateIndex(
                name: "IX_MailTaslak_KullaniciID",
                table: "MailTaslak",
                column: "KullaniciID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KisiGrup");

            migrationBuilder.DropTable(
                name: "MailDokuman");

            migrationBuilder.DropTable(
                name: "Grup");

            migrationBuilder.DropTable(
                name: "Kisi");

            migrationBuilder.DropTable(
                name: "Dokumanlar");

            migrationBuilder.DropTable(
                name: "MailTaslak");

            migrationBuilder.DropTable(
                name: "Kullanici");
        }
    }
}
