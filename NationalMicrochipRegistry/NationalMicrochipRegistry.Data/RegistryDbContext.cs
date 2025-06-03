using Microsoft.EntityFrameworkCore;
using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Data
{
    /// <summary>
    /// Represents the Entity Framework Core database context for the National Microchip Registry system.
    /// </summary>
    public class RegistryDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryDbContext"/> class using specified options.
        /// </summary>
        /// <param name="options">The options to configure the DbContext.</param>
        public RegistryDbContext(DbContextOptions<RegistryDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the table for system users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the table for clinics participating in the registry.
        /// </summary>
        public DbSet<Clinic> Clinics { get; set; }

        /// <summary>
        /// Gets or sets the table for animals registered with microchips.
        /// </summary>
        public DbSet<Animal> Animals { get; set; }

        /// <summary>
        /// Gets or sets the table for microchips.
        /// </summary>
        public DbSet<Microchip> Microchips { get; set; }

        /// <summary>
        /// Configures entity relationships and constraints using Fluent API.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure ChipNumber is unique
            modelBuilder.Entity<Microchip>()
                .HasIndex(m => m.ChipNumber)
                .IsUnique();

            // Define optional one-to-one relationship between Animal and Microchip
            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Microchip)
                .WithOne(m => m.Animal)
                .HasForeignKey<Microchip>(m => m.AnimalId)
                .IsRequired(false);
        }
    }
}
