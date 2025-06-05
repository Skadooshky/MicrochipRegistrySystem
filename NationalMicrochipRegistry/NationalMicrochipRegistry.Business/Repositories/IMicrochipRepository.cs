using NationalMicrochipRegistry.Data.Models;
using System.Collections.Generic;

namespace NationalMicrochipRegistry.Business.Repositories
{
    /// <summary>
    /// Provides methods for managing microchip records in the data store.
    /// </summary>
    public interface IMicrochipRepository
    {
        /// <summary>
        /// Adds a new microchip to the data store.
        /// </summary>
        /// <param name="chip">The <see cref="Microchip"/> object to add.</param>
        void Add(Microchip chip);

        /// <summary>
        /// Updates an existing microchip in the data store.
        /// </summary>
        /// <param name="chip">The <see cref="Microchip"/> object containing updated data.</param>
        void Update(Microchip chip);

        /// <summary>
        /// Deletes a microchip based on its chip number.
        /// </summary>
        /// <param name="chipNumber">The unique chip number of the microchip to delete.</param>
        void DeleteByNumber(string chipNumber);

        /// <summary>
        /// Retrieves a microchip by its chip number.
        /// </summary>
        /// <param name="chipNumber">The chip number used to find the microchip.</param>
        /// <returns>The <see cref="Microchip"/> if found; otherwise, <c>null</c>.</returns>
        Microchip? GetByChipNumber(string chipNumber);

        /// <summary>
        /// Retrieves all microchips in the data store.
        /// </summary>
        /// <returns>A list of all <see cref="Microchip"/> records.</returns>
        List<Microchip> GetAll();

        /// <summary>
        /// Retrieves a microchip by the associated animal’s ID.
        /// </summary>
        /// <param name="animalId">The ID of the animal whose microchip is requested.</param>
        /// <returns>The <see cref="Microchip"/> if found; otherwise, <c>null</c>.</returns>
        Microchip? GetByAnimalId(int animalId);
    }
}
