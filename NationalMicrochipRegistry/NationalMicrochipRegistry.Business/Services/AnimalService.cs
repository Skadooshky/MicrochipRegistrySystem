using NationalMicrochipRegistry.Data.Enums;
using NationalMicrochipRegistry.Data.Models;
using NationalMicrochipRegistry.Business.Repositories;

namespace NationalMicrochipRegistry.Business.Services
{
    /// <summary>
    /// Provides operations for managing animals and assigning them to microchips.
    /// </summary>
    public class AnimalService
    {
        private readonly IAnimalRepository _animalRepo;
        private readonly IMicrochipRepository _chipRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalService"/> class.
        /// </summary>
        /// <param name="animalRepo">The animal repository.</param>
        /// <param name="chipRepo">The microchip repository.</param>
        public AnimalService(IAnimalRepository animalRepo, IMicrochipRepository chipRepo)
        {
            _animalRepo = animalRepo;
            _chipRepo = chipRepo;
        }

        /// <summary>
        /// Retrieves the animal assigned to the specified microchip number.
        /// </summary>
        /// <param name="chipNumber">The chip number to look up.</param>
        /// <returns>The <see cref="Animal"/> assigned to the chip, or <c>null</c> if none is assigned.</returns>
        /// <example>
        /// <code>
        /// var animal = animalService.GetAnimalByChipNumber("ABC123456");
        /// if (animal != null)
        /// {
        ///     Console.WriteLine($"Animal name: {animal.Name}");
        /// }
        /// </code>
        /// </example>
        public Animal? GetAnimalByChipNumber(string chipNumber)
        {
            var chip = _chipRepo.GetByChipNumber(chipNumber);
            return chip?.Animal;
        }

        /// <summary>
        /// Assigns an animal to a microchip by chip number.
        /// </summary>
        /// <param name="chipNumber">The chip number to assign the animal to.</param>
        /// <param name="animal">The animal to assign.</param>
        /// <exception cref="InvalidOperationException">Thrown if the chip is not found or already assigned.</exception>
        /// <example>
        /// <code>
        /// var animal = new Animal { Name = "Luna", Type = AnimalType.Cat };
        /// animalService.AssignAnimalToChip("CHIP123", animal);
        /// </code>
        /// </example>
        public void AssignAnimalToChip(string chipNumber, Animal animal)
        {
            var chip = _chipRepo.GetByChipNumber(chipNumber);
            if (chip == null || chip.Animal != null)
                throw new InvalidOperationException("Chip not found or already assigned.");

            chip.Animal = animal;
            chip.Status = ChipStatus.Assigned;

            _animalRepo.Add(animal);
            _chipRepo.Update(chip);
        }

        /// <summary>
        /// Adds a new animal to the data store.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        /// <example>
        /// <code>
        /// var animal = new Animal { Name = "Max", Type = AnimalType.Dog };
        /// animalService.AddAnimal(animal);
        /// </code>
        /// </example>
        public void AddAnimal(Animal animal)
        {
            _animalRepo.Add(animal);
        }

        /// <summary>
        /// Retrieves all animals from the data store.
        /// </summary>
        /// <returns>A list of all <see cref="Animal"/> records.</returns>
        /// <example>
        /// <code>
        /// var allAnimals = animalService.GetAllAnimals();
        /// foreach (var animal in allAnimals)
        /// {
        ///     Console.WriteLine(animal.Name);
        /// }
        /// </code>
        /// </example>
        public List<Animal> GetAllAnimals()
        {
            return _animalRepo.GetAll();
        }
    }
}
