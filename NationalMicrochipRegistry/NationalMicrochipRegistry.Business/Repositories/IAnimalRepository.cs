using System.Collections.Generic;
using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Repositories
{
    /// <summary>
    /// Defines CRUD operations for managing animal records in the data layer.
    /// </summary>
    public interface IAnimalRepository
    {
        /// <summary>
        /// Adds a new animal record to the data store.
        /// </summary>
        /// <param name="animal">The <see cref="Animal"/> object to add.</param>
        void Add(Animal animal);

        /// <summary>
        /// Retrieves all animals from the data store.
        /// </summary>
        /// <returns>A list of <see cref="Animal"/> objects.</returns>
        List<Animal> GetAll();

        /// <summary>
        /// Retrieves a specific animal by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the animal to retrieve.</param>
        /// <returns>The corresponding <see cref="Animal"/> if found; otherwise, <c>null</c>.</returns>
        Animal? GetById(int id);

        /// <summary>
        /// Updates the details of an existing animal.
        /// </summary>
        /// <param name="animal">The updated <see cref="Animal"/> object.</param>
        void Update(Animal animal);

        /// <summary>
        /// Deletes an animal record by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the animal to delete.</param>
        void Delete(int id);
    }
}
