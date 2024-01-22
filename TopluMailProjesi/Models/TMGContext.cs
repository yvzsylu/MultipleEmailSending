using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TopluMailProjesi.Models
{
    public partial class TMGContext : DbContext
    {
        public TMGContext()
        {
        }

        public TMGContext(DbContextOptions<TMGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dokumanlar> Dokumanlars { get; set; } = null!;
        public virtual DbSet<Grup> Grups { get; set; } = null!;
        public virtual DbSet<Kisi> Kisis { get; set; } = null!;
        public virtual DbSet<KisiGrup> KisiGrups { get; set; } = null!;
        public virtual DbSet<Kullanici> Kullanicis { get; set; } = null!;
        public virtual DbSet<MailDokuman> MailDokumen { get; set; } = null!;
        public virtual DbSet<MailTaslak> MailTaslaks { get; set; } = null!;
        public virtual DbSet<SmtpAyarlari> SmtpAyarlaris { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=pc\\SQLEXPRESS;Database=TMG;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dokumanlar>(entity =>
            {
                entity.HasKey(e => e.DokumanId)
                    .HasName("PK__Dokumanl__53AEF7B0DBC12E54");

                entity.ToTable("Dokumanlar");

                entity.Property(e => e.DokumanId)
                    .HasColumnName("DokumanID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DokumanYolu).HasMaxLength(300);
            });

            modelBuilder.Entity<Grup>(entity =>
            {
                entity.ToTable("Grup");

                entity.Property(e => e.GrupId)
                    .HasColumnName("GrupID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.GrupAdi).HasMaxLength(100);

                entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");

                entity.HasOne(d => d.Kullanici)
                    .WithMany(p => p.Grups)
                    .HasForeignKey(d => d.KullaniciId)
                    .HasConstraintName("FK__Grup__KullaniciI__2F10007B");
            });

            modelBuilder.Entity<Kisi>(entity =>
            {
                entity.ToTable("Kisi");

                entity.Property(e => e.KisiId)
                    .HasColumnName("KisiID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Adi).HasMaxLength(50);

                entity.Property(e => e.Cep).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");

                entity.Property(e => e.Soyadi).HasMaxLength(50);

                entity.HasOne(d => d.Kullanici)
                    .WithMany(p => p.Kisis)
                    .HasForeignKey(d => d.KullaniciId)
                    .HasConstraintName("FK__Kisi__KullaniciI__2B3F6F97");
            });

            modelBuilder.Entity<KisiGrup>(entity =>
            {
                entity.ToTable("KisiGrup");

                entity.Property(e => e.KisiGrupId)
                    .HasColumnName("KisiGrupID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.GrupId).HasColumnName("GrupID");

                entity.Property(e => e.KisiId).HasColumnName("KisiID");

                entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");

                entity.HasOne(d => d.Grup)
                    .WithMany(p => p.KisiGrups)
                    .HasForeignKey(d => d.GrupId)
                    .HasConstraintName("FK__KisiGrup__GrupID__33D4B598");

                entity.HasOne(d => d.Kisi)
                    .WithMany(p => p.KisiGrups)
                    .HasForeignKey(d => d.KisiId)
                    .HasConstraintName("FK__KisiGrup__KisiID__32E0915F");

                entity.HasOne(d => d.Kullanici)
                    .WithMany(p => p.KisiGrups)
                    .HasForeignKey(d => d.KullaniciId)
                    .HasConstraintName("FK__KisiGrup__Kullan__34C8D9D1");
            });

            modelBuilder.Entity<Kullanici>(entity =>
            {
                entity.ToTable("Kullanici");

                entity.Property(e => e.KullaniciId)
                    .HasColumnName("KullaniciID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Adi).HasMaxLength(50);

                entity.Property(e => e.KullaniciAdi).HasMaxLength(50);

                entity.Property(e => e.Sifre).HasMaxLength(50);

                entity.Property(e => e.Soyadi).HasMaxLength(50);
            });

            modelBuilder.Entity<MailDokuman>(entity =>
            {
                entity.ToTable("MailDokuman");

                entity.Property(e => e.MailDokumanId)
                    .HasColumnName("MailDokumanID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DokumanId).HasColumnName("DokumanID");

                entity.Property(e => e.MailTaslakId).HasColumnName("MailTaslakID");

                entity.HasOne(d => d.Dokuman)
                    .WithMany(p => p.MailDokumen)
                    .HasForeignKey(d => d.DokumanId)
                    .HasConstraintName("FK__MailDokum__Dokum__3D5E1FD2");

                entity.HasOne(d => d.MailTaslak)
                    .WithMany(p => p.MailDokumen)
                    .HasForeignKey(d => d.MailTaslakId)
                    .HasConstraintName("FK__MailDokum__MailT__3C69FB99");
            });

            modelBuilder.Entity<MailTaslak>(entity =>
            {
                entity.ToTable("MailTaslak");

                entity.Property(e => e.MailTaslakId)
                    .HasColumnName("MailTaslakID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Aciklama).HasColumnType("ntext");

                entity.Property(e => e.Baslik).HasMaxLength(1000);

                entity.Property(e => e.GonderimTarihi).HasColumnType("datetime");

                entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");

                entity.Property(e => e.SorguNo).HasMaxLength(50);

                entity.HasOne(d => d.Kullanici)
                    .WithMany(p => p.MailTaslaks)
                    .HasForeignKey(d => d.KullaniciId)
                    .HasConstraintName("FK__MailTasla__Kulla__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
