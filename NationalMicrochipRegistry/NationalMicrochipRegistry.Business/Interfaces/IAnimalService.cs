using NationalMicrochipRegistry.Data.Models;
using System.Collections.Generic;

namespace NationalMicrochipRegistry.Business.Interfaces
{
    /// <summary>
    /// Provides operations for managing animals and their microchip associations.
    /// </summary>
    public interface IAnimalService
    {
        /// <summary>
        /// Adds a new animal to the data store.
        /// </summary>
        /// <param name="animal">The <see cref="Animal"/> object to add.</param>
        void AddAnimal(Animal animal);

        /// <summary>
        /// Retrieves all animals from the data store.
        /// </summary>
        /// <returns>A list of <see cref="Animal"/> objects.</returns>
        List<Animal> GetAllAnimals();

        /// <summary>
        /// Retrieves an animal associated with the specified microchip number.
        /// </summary>
        /// <param name="chipNumber">The chip number to look up.</param>
        /// <returns>The <see cref="Animal"/> assigned to the chip, or <c>null</c> if none is assigned.</returns>
        Animal? GetAnimalByChipNumber(string chipNumber);

        /// <summary>
        /// Assigns a new animal to a microchip by chip number.
        /// </summary>
        /// <param name="chipNumber">The chip number to assign the animal to.</param>
        /// <param name="animal">The <see cref="Animal"/> to assign.</param>
        void AssignAnimalToChip(string chipNumber, Animal animal);

        /// <summary>
        /// Updates an existing animal’s details in the data store.
        /// </summary>
        /// <param name="animal">The <see cref="Animal"/> object with updated data.</param>
        void UpdateAnimal(Animal animal);
    }
}
