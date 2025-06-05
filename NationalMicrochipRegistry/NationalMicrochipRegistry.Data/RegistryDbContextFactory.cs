using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NationalMicrochipRegistry.Data
{
    /// <summary>
    /// Design-time factory for creating <see cref="RegistryDbContext"/> instances using SQLite.
    /// </summary>
    public class RegistryDbContextFactory : IDesignTimeDbContextFactory<RegistryDbContext>
    {
        /// <summary>
        /// Creates a new <see cref="RegistryDbContext"/> configured to use SQLite.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        /// <returns>A new <see cref="RegistryDbContext"/> instance.</returns>
        public RegistryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RegistryDbContext>();

            // Use a SQLite file named "registry.db" in the working directory
            optionsBuilder.UseSqlite("Data Source=registry.db");

            return new RegistryDbContext(optionsBuilder.Options);
        }
    }
}
