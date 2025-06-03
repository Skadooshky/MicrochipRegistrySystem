using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Interfaces
{
    /// <summary>
    /// Provides operations for managing animals and their microchip associations.
    /// </summary>
    public interface IAnimalService
    {
        /// <summary>
        /// Retrieves an animal associated with the specified microchip number.
        /// </summary>
        /// <param name="chipNumber">The unique chip number linked to the animal.</param>
        /// <returns>
        /// The <see cref="Animal"/> object if found; otherwise, <c>null</c>.
        /// </returns>
        Animal? GetAnimalByChipNumber(string chipNumber);

        /// <summary>
        /// Assigns a new animal to a microchip identified by its chip number.
        /// </summary>
        /// <param name="chipNumber">The unique identifier of the microchip.</param>
        /// <param name="animal">The animal to be assigned to the chip.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the chip does not exist or is already assigned.
        /// </exception>
        void AssignAnimalToChip(string chipNumber, Animal animal);
    }
}
