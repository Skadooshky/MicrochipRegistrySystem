using Microsoft.EntityFrameworkCore;
using NationalMicrochipRegistry.Data;
using NationalMicrochipRegistry.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NationalMicrochipRegistry.Business.Repositories
{
    /// <summary>
    /// Entity Framework Core implementation of <see cref="IMicrochipRepository"/>.
    /// </summary>
    public class MicrochipRepository : IMicrochipRepository
    {
        /// <summary>
        /// Adds a new microchip to the data store.
        /// </summary>
        /// <param name="chip">The <see cref="Microchip"/> object to add.</param>
        public void Add(Microchip chip)
        {
            using (var context = new RegistryDbContextFactory().CreateDbContext(null!))
            {
                context.Microchips.Add(chip);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing microchip in the data store.
        /// </summary>
        /// <param name="chip">The <see cref="Microchip"/> object containing updated data.</param>
        public void Update(Microchip chip)
        {
            using (var context = new RegistryDbContextFactory().CreateDbContext(null!))
            {
                context.Microchips.Update(chip);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a microchip based on its chip number.
        /// </summary>
        /// <param name="chipNumber">The unique chip number of the microchip to delete.</param>
        public void DeleteByNumber(string chipNumber)
        {
            using (var context = new RegistryDbContextFactory().CreateDbContext(null!))
            {
                var existing = context.Microchips
                                      .FirstOrDefault(m => m.ChipNumber == chipNumber);
                if (existing != null)
                {
                    context.Microchips.Remove(existing);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Retrieves a microchip by its chip number.
        /// </summary>
        /// <param name="chipNumber">The chip number used to find the microchip.</param>
        /// <returns>
        /// The <see cref="Microchip"/> if found; otherwise, <c>null</c>.
        /// </returns>
        public Microchip? GetByChipNumber(string chipNumber)
        {
            using (var context = new RegistryDbContextFactory().CreateDbContext(null!))
            {
                return context.Microchips
                              .Include(m => m.Animal)
                              .Include(m => m.Clinic)
                              .FirstOrDefault(m => m.ChipNumber == chipNumber);
            }
        }

        /// <summary>
        /// Retrieves the microchip whose AnimalId matches the given animal ID, if any.
        /// </summary>
        /// <param name="animalId">The ID of the animal whose chip to retrieve.</param>
        /// <returns>
        /// The <see cref="Microchip"/> if found; otherwise, <c>null</c>.
        /// </returns>
        public Microchip? GetByAnimalId(int animalId)
        {
            using (var context = new RegistryDbContextFactory().CreateDbContext(null!))
            {
                return context.Microchips
                              .Include(m => m.Animal)
                              .Include(m => m.Clinic)
                              .FirstOrDefault(m => m.AnimalId == animalId);
            }
        }

        /// <summary>
        /// Retrieves all microchips from the data store.
        /// </summary>
        /// <returns>A list of all <see cref="Microchip"/> records.</returns>
        public List<Microchip> GetAll()
        {
            using (var context = new RegistryDbContextFactory().CreateDbContext(null!))
            {
                return context.Microchips
                              .Include(m => m.Animal)
                              .Include(m => m.Clinic)
                              .ToList();
            }
        }
    }
}
