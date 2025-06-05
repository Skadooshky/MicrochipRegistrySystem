using NationalMicrochipRegistry.Data.Models;
using System.Collections.Generic;

namespace NationalMicrochipRegistry.Business.Interfaces
{
    /// <summary>
    /// Provides methods for managing microchips in the system.
    /// </summary>
    public interface IMicrochipService
    {
        /// <summary>
        /// Adds a single microchip to the system.
        /// </summary>
        /// <param name="chip">The <see cref="Microchip"/> to add.</param>
        void AddMicrochip(Microchip chip);

        /// <summary>
        /// Generates a specified number of new microchips and adds them to the system.
        /// </summary>
        /// <param name="count">The number of microchips to generate.</param>
        void GenerateMicrochips(int count);

        /// <summary>
        /// Deletes a microchip from the system using its chip number.
        /// </summary>
        /// <param name="chipNumber">The unique chip number of the microchip to delete.</param>
        void DeleteMicrochip(string chipNumber);

        /// <summary>
        /// Retrieves a microchip by its chip number.
        /// </summary>
        /// <param name="chipNumber">The unique chip number to search for.</param>
        /// <returns>The <see cref="Microchip"/> if found; otherwise, <c>null</c>.</returns>
        Microchip? GetMicrochipByNumber(string chipNumber);

        /// <summary>
        /// Retrieves a list of all microchips in the system.
        /// </summary>
        /// <returns>A list of <see cref="Microchip"/> objects.</returns>
        List<Microchip> GetAllMicrochips();

        /// <summary>
        /// Retrieves the microchip assigned to a given animal, if any.
        /// </summary>
        /// <param name="animalId">The ID of the animal whose microchip is requested.</param>
        /// <returns>The <see cref="Microchip"/> if found; otherwise, <c>null</c>.</returns>
        Microchip? GetMicrochipByAnimalId(int animalId);

        /// <summary>
        /// Updates an existing microchip’s details (for example, assigning AnimalId or ClinicId).
        /// </summary>
        /// <param name="chip">The <see cref="Microchip"/> object with updated data.</param>
        void UpdateMicrochip(Microchip chip);
    }
}
