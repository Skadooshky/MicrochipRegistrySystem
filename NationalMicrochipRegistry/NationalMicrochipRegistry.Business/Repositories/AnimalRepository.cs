using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NationalMicrochipRegistry.Data;
using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Repositories
{
    /// <summary>
    /// Implements CRUD operations for managing animal records in the data layer.
    /// </summary>
    public class AnimalRepository : IAnimalRepository
    {
        private readonly RegistryDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalRepository"/> class.
        /// </summary>
        public AnimalRepository()
        {
            _context = new RegistryDbContext(CreateOptions());
        }

        /// <summary>
        /// Adds a new animal record to the data store.
        /// </summary>
        /// <param name="animal">The <see cref="Animal"/> object to add.</param>
        public void Add(Animal animal)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves all animals from the data store, including their microchips and clinics.
        /// </summary>
        /// <returns>A list of <see cref="Animal"/> objects.</returns>
        public List<Animal> GetAll()
        {
            return _context.Animals
                           .Include(a => a.Microchip!)
                           .ThenInclude(m => m.Clinic)
                           .ToList();
        }

        /// <summary>
        /// Retrieves a specific animal by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the animal to retrieve.</param>
        /// <returns>The corresponding <see cref="Animal"/> if found; otherwise, <c>null</c>.</returns>
        public Animal? GetById(int id)
        {
            return _context.Animals
                           .Include(a => a.Microchip!)
                           .ThenInclude(m => m.Clinic)
                           .SingleOrDefault(a => a.Id == id);
        }

        /// <summary>
        /// Updates the details of an existing animal.
        /// </summary>
        /// <param name="animal">The <see cref="Animal"/> object containing updated data.</param>
        public void Update(Animal animal)
        {
            // Load the existing tracked entity from this same DbContext
            var existing = _context.Animals
                                   .Include(a => a.Microchip!)
                                   .ThenInclude(m => m.Clinic)
                                   .SingleOrDefault(a => a.Id == animal.Id);
            if (existing == null)
                return; // Or throw new KeyNotFoundException($"Animal with ID {animal.Id} not found.");

            // Copy over scalar properties (not the Microchip navigation)
            existing.Name = animal.Name;
            existing.Type = animal.Type;
            existing.Breed = animal.Breed;
            existing.Sex = animal.Sex;
            existing.Age = animal.Age;
            existing.OwnerName = animal.OwnerName;
            existing.OwnerContact = animal.OwnerContact;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                // Re‐throw including inner‐exception message for visibility
                throw new DbUpdateException("Failed to update Animal. See inner exception for details.", dbEx);
            }
        }

        /// <summary>
        /// Deletes an animal record by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the animal to delete.</param>
        public void Delete(int id)
        {
            var toDelete = _context.Animals.Find(id);
            if (toDelete != null)
            {
                _context.Animals.Remove(toDelete);
                _context.SaveChanges();
            }
        }

        private static DbContextOptions<RegistryDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<RegistryDbContext>()
                .UseSqlite("Data Source=registry.db")
                .Options;
        }
    }
}
