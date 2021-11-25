using System;
using E_Veterinar.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace E_Veterinar.Data
{
    public partial class eveterinarContext : IdentityDbContext<ApplicationUser>
    {
        public eveterinarContext()
        {
        }

        public eveterinarContext(DbContextOptions<eveterinarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Evidenca> Evidencas { get; set; } = null!;
        public virtual DbSet<Izdelek> Izdeleks { get; set; } = null!;
        public virtual DbSet<Narocilo> Narocilos { get; set; } = null!;
        public virtual DbSet<Postum> Posta { get; set; } = null!;
        public virtual DbSet<Storitev> Storitevs { get; set; } = null!;
        public virtual DbSet<Stranka> Strankas { get; set; } = null!;
        public virtual DbSet<Termin> Termins { get; set; } = null!;
        public virtual DbSet<Veterinar> Veterinars { get; set; } = null!;
        public virtual DbSet<Zaloga> Zalogas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=eveterinar;User Id=sa;Password=StrongPassw0rd!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evidenca>(entity =>
            {
                entity.HasKey(e => e.IdEvidence)
                    .IsClustered(false);

                entity.ToTable("EVIDENCA");

                entity.HasIndex(e => e.IdNarocilo, "JE_BILO_OPRAVLJENO_FK");

                entity.HasIndex(e => new { e.IdVeterinar, e.DatumZacetka, e.DatumKonca }, "JE_ZABELEZENO_V_FK");

                entity.Property(e => e.IdEvidence)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_EVIDENCE");

                entity.Property(e => e.Cena)
                    .HasColumnType("money")
                    .HasColumnName("CENA");

                entity.Property(e => e.DatumKonca)
                    .HasColumnType("datetime")
                    .HasColumnName("DATUM_KONCA");

                entity.Property(e => e.DatumZacetka)
                    .HasColumnType("datetime")
                    .HasColumnName("DATUM_ZACETKA");

                entity.Property(e => e.IdNarocilo)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_NAROCILO");

                entity.Property(e => e.IdVeterinar)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_VETERINAR");

                entity.HasOne(d => d.IdNarociloNavigation)
                    .WithMany(p => p.Evidencas)
                    .HasForeignKey(d => d.IdNarocilo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVIDENCA_JE_BILO_O_NAROCILO");

                entity.HasOne(d => d.Termin)
                    .WithMany(p => p.Evidencas)
                    .HasForeignKey(d => new { d.IdVeterinar, d.DatumZacetka, d.DatumKonca })
                    .HasConstraintName("FK_EVIDENCA_JE_ZABELE_TERMIN");
            });

            modelBuilder.Entity<Izdelek>(entity =>
            {
                entity.HasKey(e => e.IdIzdelek)
                    .IsClustered(false);

                entity.ToTable("IZDELEK");

                entity.Property(e => e.IdIzdelek)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_IZDELEK");

                entity.Property(e => e.Cena)
                    .HasColumnType("money")
                    .HasColumnName("CENA");

                entity.Property(e => e.Ime)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IME");

                entity.Property(e => e.Opis)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("OPIS");
            });

            modelBuilder.Entity<Narocilo>(entity =>
            {
                entity.HasKey(e => e.IdNarocilo)
                    .IsClustered(false);

                entity.ToTable("NAROCILO");

                entity.HasIndex(e => e.IdStranka, "JE_NAROCILA_FK");

                entity.Property(e => e.IdNarocilo)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_NAROCILO");

                entity.Property(e => e.DatumNarocila)
                    .HasColumnType("datetime")
                    .HasColumnName("DATUM_NAROCILA");

                entity.Property(e => e.IdStranka)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_STRANKA");

                entity.Property(e => e.ZahtevanaKolicina)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ZAHTEVANA_KOLICINA");

                entity.HasOne(d => d.IdStrankaNavigation)
                    .WithMany(p => p.Narocilos)
                    .HasForeignKey(d => d.IdStranka)
                    .HasConstraintName("FK_NAROCILO_JE_NAROCI_STRANKA");
            });

            modelBuilder.Entity<Postum>(entity =>
            {
                entity.HasKey(e => e.Stevilka)
                    .IsClustered(false);

                entity.ToTable("POSTA");

                entity.Property(e => e.Stevilka)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("STEVILKA");

                entity.Property(e => e.Naziv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAZIV");
            });

            modelBuilder.Entity<Storitev>(entity =>
            {
                entity.HasKey(e => e.IdStoritev)
                    .IsClustered(false);

                entity.ToTable("STORITEV");

                entity.Property(e => e.IdStoritev)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_STORITEV");

                entity.Property(e => e.OpisStoritve)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("OPIS_STORITVE");

                entity.HasMany(d => d.IdEvidences)
                    .WithMany(p => p.IdStoritevs)
                    .UsingEntity<Dictionary<string, object>>(
                        "JeBilaOpravljena",
                        l => l.HasOne<Evidenca>().WithMany().HasForeignKey("IdEvidence").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_JE_BILA__JE_BILA_O_EVIDENCA"),
                        r => r.HasOne<Storitev>().WithMany().HasForeignKey("IdStoritev").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_JE_BILA__JE_BILA_O_STORITEV"),
                        j =>
                        {
                            j.HasKey("IdStoritev", "IdEvidence");

                            j.ToTable("JE_BILA_OPRAVLJENA");

                            j.HasIndex(new[] { "IdEvidence" }, "JE_BILA_OPRAVLJENA2_FK");

                            j.HasIndex(new[] { "IdStoritev" }, "JE_BILA_OPRAVLJENA_FK");

                            j.IndexerProperty<decimal>("IdStoritev").HasColumnType("numeric(18, 0)").HasColumnName("ID_STORITEV");

                            j.IndexerProperty<decimal>("IdEvidence").HasColumnType("numeric(18, 0)").HasColumnName("ID_EVIDENCE");
                        });
            });

            modelBuilder.Entity<Stranka>(entity =>
            {
                entity.HasKey(e => e.IdStranka)
                    .IsClustered(false);

                entity.ToTable("STRANKA");

                entity.HasIndex(e => e.Stevilka, "JE_NA_FK");

                entity.Property(e => e.IdStranka)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_STRANKA");

                entity.Property(e => e.Ime)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IME");

                entity.Property(e => e.Kraj)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("KRAJ");

                entity.Property(e => e.Naslov)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NASLOV");

                entity.Property(e => e.Priimek)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PRIIMEK");

                entity.Property(e => e.Stevilka)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("STEVILKA");

                entity.HasOne(d => d.StevilkaNavigation)
                    .WithMany(p => p.Strankas)
                    .HasForeignKey(d => d.Stevilka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STRANKA_JE_NA_POSTA");
            });

            modelBuilder.Entity<Termin>(entity =>
            {
                entity.HasKey(e => new { e.IdVeterinar, e.DatumZacetka, e.DatumKonca })
                    .IsClustered(false);

                entity.ToTable("TERMIN");

                entity.HasIndex(e => e.IdStranka, "JE_PREVZELA_FK");

                entity.HasIndex(e => e.IdVeterinar, "JE_RAZPISAL_FK");

                entity.Property(e => e.IdVeterinar)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_VETERINAR");

                entity.Property(e => e.DatumZacetka)
                    .HasColumnType("datetime")
                    .HasColumnName("DATUM_ZACETKA");

                entity.Property(e => e.DatumKonca)
                    .HasColumnType("datetime")
                    .HasColumnName("DATUM_KONCA");

                entity.Property(e => e.IdStranka)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_STRANKA");

                entity.Property(e => e.JePotrjen).HasColumnName("JE_POTRJEN");

                entity.Property(e => e.JeZaseden).HasColumnName("JE_ZASEDEN");

                entity.HasOne(d => d.IdStrankaNavigation)
                    .WithMany(p => p.Termins)
                    .HasForeignKey(d => d.IdStranka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TERMIN_JE_PREVZE_STRANKA");

                entity.HasOne(d => d.IdVeterinarNavigation)
                    .WithMany(p => p.Termins)
                    .HasForeignKey(d => d.IdVeterinar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TERMIN_JE_RAZPIS_VETERINA");
            });

            modelBuilder.Entity<Veterinar>(entity =>
            {
                entity.HasKey(e => e.IdVeterinar)
                    .IsClustered(false);

                entity.ToTable("VETERINAR");

                entity.HasIndex(e => e.Stevilka, "IMA_VETERINO_NA_FK");

                entity.Property(e => e.IdVeterinar)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_VETERINAR");

                entity.Property(e => e.Ime)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IME");

                entity.Property(e => e.Kraj)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("KRAJ");

                entity.Property(e => e.NaDom).HasColumnName("NA_DOM");

                entity.Property(e => e.Priimek)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PRIIMEK");

                entity.Property(e => e.Stevilka)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("STEVILKA");

                entity.HasOne(d => d.StevilkaNavigation)
                    .WithMany(p => p.Veterinars)
                    .HasForeignKey(d => d.Stevilka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VETERINA_IMA_VETER_POSTA");
            });

            modelBuilder.Entity<Zaloga>(entity =>
            {
                entity.HasKey(e => new { e.IdIzdelek, e.IdVeterinar });

                entity.ToTable("ZALOGA");

                entity.HasIndex(e => e.IdVeterinar, "IMA_FK");

                entity.HasIndex(e => e.IdIzdelek, "JE_OD_FK");

                entity.Property(e => e.IdIzdelek)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_IZDELEK");

                entity.Property(e => e.IdVeterinar)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID_VETERINAR");

                entity.Property(e => e.Kolicina)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("KOLICINA");

                entity.HasOne(d => d.IdIzdelekNavigation)
                    .WithMany(p => p.Zalogas)
                    .HasForeignKey(d => d.IdIzdelek)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZALOGA_JE_OD_IZDELEK");

                entity.HasOne(d => d.IdVeterinarNavigation)
                    .WithMany(p => p.Zalogas)
                    .HasForeignKey(d => d.IdVeterinar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZALOGA_IMA_VETERINA");

                entity.HasMany(d => d.IdNarocilos)
                    .WithMany(p => p.Ids)
                    .UsingEntity<Dictionary<string, object>>(
                        "Zahteva",
                        l => l.HasOne<Narocilo>().WithMany().HasForeignKey("IdNarocilo").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ZAHTEVA_ZAHTEVA_NAROCILO"),
                        r => r.HasOne<Zaloga>().WithMany().HasForeignKey("IdIzdelek", "IdVeterinar").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ZAHTEVA_ZAHTEVA2_ZALOGA"),
                        j =>
                        {
                            j.HasKey("IdIzdelek", "IdNarocilo", "IdVeterinar");

                            j.ToTable("ZAHTEVA");

                            j.HasIndex(new[] { "IdIzdelek", "IdVeterinar" }, "ZAHTEVA2_FK");

                            j.HasIndex(new[] { "IdNarocilo" }, "ZAHTEVA_FK");

                            j.IndexerProperty<decimal>("IdIzdelek").HasColumnType("numeric(18, 0)").HasColumnName("ID_IZDELEK");

                            j.IndexerProperty<decimal>("IdNarocilo").HasColumnType("numeric(18, 0)").HasColumnName("ID_NAROCILO");

                            j.IndexerProperty<decimal>("IdVeterinar").HasColumnType("numeric(18, 0)").HasColumnName("ID_VETERINAR");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
