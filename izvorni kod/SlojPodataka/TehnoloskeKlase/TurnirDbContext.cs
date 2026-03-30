using Microsoft.EntityFrameworkCore;
using SlojPodataka.KlasePodataka;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace SlojPodataka.TehnoloskeKlase
{
    public class TurnirDbContext : DbContext
    {
        public TurnirDbContext(DbContextOptions<TurnirDbContext> options) : base(options) { }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Klub> Klubovi { get; set; }
        public DbSet<Zapisnik> Zapisnici { get; set; }
        public DbSet<StavkaZapisnika> StavkeZapisnika { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zapisnik>()
                .HasOne(z => z.Domacin)
                .WithMany(k => k.DomacinUtakmice)
                .HasForeignKey(z => z.DomacinID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Zapisnik>()
                .HasOne(z => z.Gost)
                .WithMany(k => k.GostUtakmice)
                .HasForeignKey(z => z.GostID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StavkaZapisnika>()
                .HasOne(s => s.Klub)
                .WithMany(k => k.Golovi)
                .HasForeignKey(s => s.KlubID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
