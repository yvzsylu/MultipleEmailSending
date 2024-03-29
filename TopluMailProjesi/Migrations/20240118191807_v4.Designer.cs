﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopluMailProjesi.Models;

#nullable disable

namespace TopluMailProjesi.Migrations
{
    [DbContext(typeof(TMGContext))]
    [Migration("20240118191807_v4")]
    partial class v4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TopluMailProjesi.Models.Dokumanlar", b =>
                {
                    b.Property<Guid>("DokumanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("DokumanID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("DokumanYolu")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid?>("KullaniciId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DokumanId")
                        .HasName("PK__Dokumanl__53AEF7B0DBC12E54");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Dokumanlar", (string)null);
                });

            modelBuilder.Entity("TopluMailProjesi.Models.Grup", b =>
                {
                    b.Property<Guid>("GrupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("GrupID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("GrupAdi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("KullaniciId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("KullaniciID");

                    b.HasKey("GrupId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Grup", (string)null);
                });

            modelBuilder.Entity("TopluMailProjesi.Models.Kisi", b =>
                {
                    b.Property<Guid>("KisiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("KisiID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("KullaniciId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("KullaniciID");

                    b.Property<string>("Soyadi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("KisiId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Kisi", (string)null);
                });

            modelBuilder.Entity("TopluMailProjesi.Models.KisiGrup", b =>
                {
                    b.Property<Guid>("KisiGrupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("KisiGrupID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<Guid?>("GrupId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("GrupID");

                    b.Property<Guid?>("KisiId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("KisiID");

                    b.Property<Guid?>("KullaniciId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("KullaniciID");

                    b.HasKey("KisiGrupId");

                    b.HasIndex("GrupId");

                    b.HasIndex("KisiId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("KisiGrup", (string)null);
                });

            modelBuilder.Entity("TopluMailProjesi.Models.Kullanici", b =>
                {
                    b.Property<Guid>("KullaniciId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("KullaniciID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("IslemSayisi")
                        .HasColumnType("int");

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Soyadi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("KullaniciId");

                    b.ToTable("Kullanici", (string)null);
                });

            modelBuilder.Entity("TopluMailProjesi.Models.MailDokuman", b =>
                {
                    b.Property<Guid>("MailDokumanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("MailDokumanID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<Guid?>("DokumanId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("DokumanID");

                    b.Property<Guid?>("MailTaslakId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("MailTaslakID");

                    b.HasKey("MailDokumanId");

                    b.HasIndex("DokumanId");

                    b.HasIndex("MailTaslakId");

                    b.ToTable("MailDokuman", (string)null);
                });

            modelBuilder.Entity("TopluMailProjesi.Models.MailTaslak", b =>
                {
                    b.Property<Guid>("MailTaslakId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("MailTaslakID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("GonderimTarihi")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("KullaniciId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("KullaniciID");

                    b.Property<string>("SorguNo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MailTaslakId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("MailTaslak", (string)null);
                });

            modelBuilder.Entity("TopluMailProjesi.Models.Dokumanlar", b =>
                {
                    b.HasOne("TopluMailProjesi.Models.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId");

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("TopluMailProjesi.Models.Grup", b =>
                {
                    b.HasOne("TopluMailProjesi.Models.Kullanici", "Kullanici")
                        .WithMany("Grups")
                        .HasForeignKey("KullaniciId")
                        .HasConstraintName("FK__Grup__KullaniciI__2F10007B");

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("TopluMailProjesi.Models.Kisi", b =>
                {
                    b.HasOne("TopluMailProjesi.Models.Kullanici", "Kullanici")
                        .WithMany("Kisis")
                        .HasForeignKey("KullaniciId")
                        .HasConstraintName("FK__Kisi__KullaniciI__2B3F6F97");

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("TopluMailProjesi.Models.KisiGrup", b =>
                {
                    b.HasOne("TopluMailProjesi.Models.Grup", "Grup")
                        .WithMany("KisiGrups")
                        .HasForeignKey("GrupId")
                        .HasConstraintName("FK__KisiGrup__GrupID__33D4B598");

                    b.HasOne("TopluMailProjesi.Models.Kisi", "Kisi")
                        .WithMany("KisiGrups")
                        .HasForeignKey("KisiId")
                        .HasConstraintName("FK__KisiGrup__KisiID__32E0915F");

                    b.HasOne("TopluMailProjesi.Models.Kullanici", "Kullanici")
                        .WithMany("KisiGrups")
                        .HasForeignKey("KullaniciId")
                        .HasConstraintName("FK__KisiGrup__Kullan__34C8D9D1");

                    b.Navigation("Grup");

                    b.Navigation("Kisi");

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("TopluMailProjesi.Models.MailDokuman", b =>
                {
                    b.HasOne("TopluMailProjesi.Models.Dokumanlar", "Dokuman")
                        .WithMany("MailDokumen")
                        .HasForeignKey("DokumanId")
                        .HasConstraintName("FK__MailDokum__Dokum__3D5E1FD2");

                    b.HasOne("TopluMailProjesi.Models.MailTaslak", "MailTaslak")
                        .WithMany("MailDokumen")
                        .HasForeignKey("MailTaslakId")
                        .HasConstraintName("FK__MailDokum__MailT__3C69FB99");

                    b.Navigation("Dokuman");

                    b.Navigation("MailTaslak");
                });

            modelBuilder.Entity("TopluMailProjesi.Models.MailTaslak", b =>
                {
                    b.HasOne("TopluMailProjesi.Models.Kullanici", "Kullanici")
                        .WithMany("MailTaslaks")
                        .HasForeignKey("KullaniciId")
                        .HasConstraintName("FK__MailTasla__Kulla__38996AB5");

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("TopluMailProjesi.Models.Dokumanlar", b =>
                {
                    b.Navigation("MailDokumen");
                });

            modelBuilder.Entity("TopluMailProjesi.Models.Grup", b =>
                {
                    b.Navigation("KisiGrups");
                });

            modelBuilder.Entity("TopluMailProjesi.Models.Kisi", b =>
                {
                    b.Navigation("KisiGrups");
                });

            modelBuilder.Entity("TopluMailProjesi.Models.Kullanici", b =>
                {
                    b.Navigation("Grups");

                    b.Navigation("KisiGrups");

                    b.Navigation("Kisis");

                    b.Navigation("MailTaslaks");
                });

            modelBuilder.Entity("TopluMailProjesi.Models.MailTaslak", b =>
                {
                    b.Navigation("MailDokumen");
                });
#pragma warning restore 612, 618
        }
    }
}
