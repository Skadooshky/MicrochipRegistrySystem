using NationalMicrochipRegistry.Data.Enums;
using NationalMicrochipRegistry.Data.Models;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;

namespace NationalMicrochipRegistry.Business.Services
{
    /// <summary>
    /// Provides services for managing microchip records.
    /// </summary>
    public class MicrochipService : IMicrochipService
    {
        private readonly IMicrochipRepository _chipRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MicrochipService"/> class.
        /// </summary>
        /// <param name="chipRepo">The microchip repository.</param>
        public MicrochipService(IMicrochipRepository chipRepo)
        {
            _chipRepo = chipRepo;
        }

        /// <summary>
        /// Adds a specific microchip to the repository.
        /// </summary>
        /// <param name="chip">The microchip to add.</param>
        /// <example>
        /// <code>
        /// var chip = new Microchip
        /// {
        ///     ChipNumber = "ABC123456",
        ///     Status = ChipStatus.Available
        /// };
        /// microchipService.AddMicrochip(chip);
        /// </code>
        /// </example>
        public void AddMicrochip(Microchip chip)
        {
            _chipRepo.Add(chip);
        }

        /// <summary>
        /// Generates a specified number of new microchips with unique chip numbers and default status.
        /// </summary>
        /// <param name="count">The number of microchips to generate.</param>
        /// <example>
        /// <code>
        /// // Generate 10 new microchips
        /// microchipService.GenerateMicrochips(10);
        /// </code>
        /// </example>
        public void GenerateMicrochips(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var chip = new Microchip
                {
                    ChipNumber = Guid.NewGuid().ToString(),
                    Status = ChipStatus.Available
                };
                _chipRepo.Add(chip);
            }
        }

        /// <summary>
        /// Deletes a microchip from the repository by its chip number.
        /// </summary>
        /// <param name="chipNumber">The unique chip number of the microchip to delete.</param>
        /// <example>
        /// <code>
        /// // Remove chip with the number "ABC123456"
        /// microchipService.DeleteMicrochip("ABC123456");
        /// </code>
        /// </example>
        public void DeleteMicrochip(string chipNumber) => _chipRepo.DeleteByNumber(chipNumber);

        /// <summary>
        /// Retrieves a microchip by its chip number.
        /// </summary>
        /// <param name="chipNumber">The unique chip number to search for.</param>
        /// <returns>The <see cref="Microchip"/> if found; otherwise, <c>null</c>.</returns>
        /// <example>
        /// <code>
        /// var chip = microchipService.GetMicrochipByNumber("ABC123456");
        /// if (chip != null)
        /// {
        ///     Console.WriteLine($"Chip status: {chip.Status}");
        /// }
        /// </code>
        /// </example>
        public Microchip? GetMicrochipByNumber(string chipNumber) => _chipRepo.GetByChipNumber(chipNumber);
    }
}
