using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Interfaces
{
    /// <summary>
    /// Provides methods for managing microchips in the system.
    /// </summary>
    public interface IMicrochipService
    {
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
        /// <returns>
        /// The <see cref="Microchip"/> if found; otherwise, <c>null</c>.
        /// </returns>
        Microchip? GetMicrochipByNumber(string chipNumber);
    }
}
