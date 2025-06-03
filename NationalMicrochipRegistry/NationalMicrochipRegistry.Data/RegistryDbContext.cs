using Microsoft.EntityFrameworkCore;
using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Data
{
    public class RegistryDbContext : DbContext
    {
        public RegistryDbContext(DbContextOptions<RegistryDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Microchip> Microchips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Microchip>()
                .HasIndex(m => m.ChipNumber)
                .IsUnique();

            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Microchip)
                .WithOne(m => m.Animal)
                .HasForeignKey<Microchip>(m => m.AnimalId)
                .IsRequired(false);
        }
    }
}
