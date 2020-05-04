using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarMechanic.Core.DomainModel.Models
{
    public partial class CarMechanicContext : DbContext
    {
        public CarMechanicContext()
        {

        }

        public CarMechanicContext(DbContextOptions<CarMechanicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alkatreszek> Alkatreszek { get; set; }
        public virtual DbSet<FelhasznaloJogok> FelhasznaloJogok { get; set; }
        public virtual DbSet<Felhasznalok> Felhasznalok { get; set; }
        public virtual DbSet<Jogok> Jogok { get; set; }
        public virtual DbSet<KozteruletJellegek> KozteruletJellegek { get; set; }
        public virtual DbSet<MunkalapRendelesek> MunkalapRendelesek { get; set; }
        public virtual DbSet<MunkalapTetelek> MunkalapTetelek { get; set; }
        public virtual DbSet<Munkalapok> Munkalapok { get; set; }
        public virtual DbSet<Szolgaltatasok> Szolgaltatasok { get; set; }
        public virtual DbSet<Telepulesek> Telepulesek { get; set; }
        public virtual DbSet<Ugyfelek> Ugyfelek { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alkatreszek>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Beszerar)
                    .HasColumnName("beszerar")
                    .HasColumnType("money");

                entity.Property(e => e.Eladasiar)
                    .HasColumnName("eladasiar")
                    .HasColumnType("money");

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasColumnName("nev")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Rogzitette).HasColumnName("rogzitette");

                entity.Property(e => e.Rogzitve)
                    .HasColumnName("rogzitve")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.RogzitetteNavigation)
                    .WithMany(p => p.Alkatreszek)
                    .HasForeignKey(d => d.Rogzitette)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alkatreszek_Felhasznalok");
            });

            modelBuilder.Entity<FelhasznaloJogok>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Felhasznaloid).HasColumnName("felhasznaloid");

                entity.Property(e => e.Jogid).HasColumnName("jogid");

                entity.HasOne(d => d.Felhasznalo)
                    .WithMany(p => p.FelhasznaloJogok)
                    .HasForeignKey(d => d.Felhasznaloid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FelhasznaloJogok_Felhasznalok");

                entity.HasOne(d => d.Jog)
                    .WithMany(p => p.FelhasznaloJogok)
                    .HasForeignKey(d => d.Jogid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FelhasznaloJogok_Jogok");
            });

            modelBuilder.Entity<Felhasznalok>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Jelszo)
                    .IsRequired()
                    .HasColumnName("jelszo")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Loginnev)
                    .IsRequired()
                    .HasColumnName("loginnev")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasColumnName("nev")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Jogok>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasColumnName("nev")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KozteruletJellegek>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasColumnName("nev")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MunkalapRendelesek>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Alkatreszid).HasColumnName("alkatreszid");

                entity.Property(e => e.Mennyiseg).HasColumnName("mennyiseg");

                entity.Property(e => e.Munkalapid).HasColumnName("munkalapid");

                entity.Property(e => e.Rogzitette).HasColumnName("rogzitette");

                entity.Property(e => e.Rogzitve)
                    .HasColumnName("rogzitve")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Alkatresz)
                    .WithMany(p => p.MunkalapRendelesek)
                    .HasForeignKey(d => d.Alkatreszid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MunkalapRendelesek_Alkatreszek");

                entity.HasOne(d => d.Munkalap)
                    .WithMany(p => p.MunkalapRendelesek)
                    .HasForeignKey(d => d.Munkalapid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MunkalapRendelesek_Munkalapok");

                entity.HasOne(d => d.RogzitetteNavigation)
                    .WithMany(p => p.MunkalapRendelesek)
                    .HasForeignKey(d => d.Rogzitette)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MunkalapRendelesek_Felhasznalok");
            });

            modelBuilder.Entity<MunkalapTetelek>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Alkatreszid).HasColumnName("alkatreszid");

                entity.Property(e => e.Ar)
                    .HasColumnName("ar")
                    .HasColumnType("money");

                entity.Property(e => e.Mennyiseg).HasColumnName("mennyiseg");

                entity.Property(e => e.Munkalapid).HasColumnName("munkalapid");

                entity.Property(e => e.Rogzitette).HasColumnName("rogzitette");

                entity.Property(e => e.Rogzitve)
                    .HasColumnName("rogzitve")
                    .HasColumnType("datetime");

                entity.Property(e => e.Szolgaltatasid).HasColumnName("szolgaltatasid");

                entity.HasOne(d => d.Alkatresz)
                    .WithMany(p => p.MunkalapTetelek)
                    .HasForeignKey(d => d.Alkatreszid)
                    .HasConstraintName("FK_MunkalapTetelek_Alkatreszek");

                entity.HasOne(d => d.Munkalap)
                    .WithMany(p => p.MunkalapTetelek)
                    .HasForeignKey(d => d.Munkalapid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MunkalapTetelek_Munkalapok");

                entity.HasOne(d => d.RogzitetteNavigation)
                    .WithMany(p => p.MunkalapTetelek)
                    .HasForeignKey(d => d.Rogzitette)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MunkalapTetelek_Felhasznalok");

                entity.HasOne(d => d.Szolgaltatas)
                    .WithMany(p => p.MunkalapTetelek)
                    .HasForeignKey(d => d.Szolgaltatasid)
                    .HasConstraintName("FK_MunkalapTetelek_Szolgaltatasok");
            });

            modelBuilder.Entity<Munkalapok>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Idopont)
                    .HasColumnName("idopont")
                    .HasColumnType("datetime");

                entity.Property(e => e.Lezarta).HasColumnName("lezarta");

                entity.Property(e => e.Lezarva)
                    .HasColumnName("lezarva")
                    .HasColumnType("datetime");

                entity.Property(e => e.Rogzitette).HasColumnName("rogzitette");

                entity.Property(e => e.Rogzitve)
                    .HasColumnName("rogzitve")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ugyfelid).HasColumnName("ugyfelid");

                entity.HasOne(d => d.LezartaNavigation)
                    .WithMany(p => p.MunkalapokLezartaNavigation)
                    .HasForeignKey(d => d.Lezarta)
                    .HasConstraintName("FK_Munkalapok_Felhasznalok");

                entity.HasOne(d => d.RogzitetteNavigation)
                    .WithMany(p => p.MunkalapokRogzitetteNavigation)
                    .HasForeignKey(d => d.Rogzitette)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Munkalapok_Felhasznalok1");

                entity.HasOne(d => d.Ugyfel)
                    .WithMany(p => p.Munkalapok)
                    .HasForeignKey(d => d.Ugyfelid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Munkalapok_Ugyfelek");
            });

            modelBuilder.Entity<Szolgaltatasok>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Egysegar)
                    .HasColumnName("egysegar")
                    .HasColumnType("money");

                entity.Property(e => e.Ismetlodesiidoszak).HasColumnName("ismetlodesiidoszak");

                entity.Property(e => e.Ismetlodo).HasColumnName("ismetlodo");

                entity.Property(e => e.Me)
                    .IsRequired()
                    .HasColumnName("me")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasColumnName("nev")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Rogzitette).HasColumnName("rogzitette");

                entity.Property(e => e.Rogzitve)
                    .HasColumnName("rogzitve")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.RogzitetteNavigation)
                    .WithMany(p => p.Szolgaltatasok)
                    .HasForeignKey(d => d.Rogzitette)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Szolgaltatasok_Felhasznalok");
            });

            modelBuilder.Entity<Telepulesek>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Irszam)
                    .IsRequired()
                    .HasColumnName("irszam")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasColumnName("nev")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ugyfelek>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adoszam)
                    .HasColumnName("adoszam")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Hazszam)
                    .IsRequired()
                    .HasColumnName("hazszam")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Kozteruletjellegid).HasColumnName("kozteruletjellegid");

                entity.Property(e => e.Kozteruletneve)
                    .IsRequired()
                    .HasColumnName("kozteruletneve")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasColumnName("nev")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Rogzitette).HasColumnName("rogzitette");

                entity.Property(e => e.Rogzitve)
                    .HasColumnName("rogzitve")
                    .HasColumnType("datetime");

                entity.Property(e => e.Telefonszam)
                    .IsRequired()
                    .HasColumnName("telefonszam")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telepulesid).HasColumnName("telepulesid");

                entity.HasOne(d => d.Kozteruletjelleg)
                    .WithMany(p => p.Ugyfelek)
                    .HasForeignKey(d => d.Kozteruletjellegid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ugyfelek_KozteruletJellegek");

                entity.HasOne(d => d.RogzitetteNavigation)
                    .WithMany(p => p.Ugyfelek)
                    .HasForeignKey(d => d.Rogzitette)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ugyfelek_Felhasznalok");

                entity.HasOne(d => d.Telepules)
                    .WithMany(p => p.Ugyfelek)
                    .HasForeignKey(d => d.Telepulesid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ugyfelek_Telepulesek");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
