using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NationalMicrochipRegistry.Data;
using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Repositories
{
    /// <summary>
    /// EF‐Core implementation of <see cref="IClinicRepository"/>, using <see cref="RegistryDbContext"/>.
    /// </summary>
    public class ClinicRepository : IClinicRepository
    {
        private readonly RegistryDbContext _context;

        /// <summary>
        /// Initializes a new instance of <see cref="ClinicRepository"/>.
        /// </summary>
        public ClinicRepository()
        {
            _context = new RegistryDbContext(CreateOptions());
        }

        /// <inheritdoc/>
        public void Add(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            _context.SaveChanges();
        }

        /// <inheritdoc/>
        public void Update(Clinic clinic)
        {
            _context.Clinics.Update(clinic);
            _context.SaveChanges();
        }

        /// <inheritdoc/>
        public void Delete(int clinicId)
        {
            var existing = _context.Clinics.Find(clinicId);
            if (existing != null)
            {
                _context.Clinics.Remove(existing);
                _context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public Clinic? GetById(int clinicId)
        {
            return _context.Clinics
                           .Include(c => c.Microchips)
                           .FirstOrDefault(c => c.Id == clinicId);
        }

        /// <inheritdoc/>
        public List<Clinic> GetAll()
        {
            return _context.Clinics
                           .Include(c => c.Microchips)
                           .ToList();
        }

        /// <summary>
        /// Builds <see cref="DbContextOptions{RegistryDbContext}"/> using a connection string.
        /// </summary>
        private static DbContextOptions<RegistryDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<RegistryDbContext>()
                .UseSqlite("Data Source=registry.db")
                .Options;
        }
    }
}
