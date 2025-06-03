using NationalMicrochipRegistry.Data.Enums;
using NationalMicrochipRegistry.Data.Models;
using NationalMicrochipRegistry.Business.Repositories;

namespace NationalMicrochipRegistry.Business.Services
{
    public class AnimalService
    {
        private readonly IAnimalRepository _animalRepo;
        private readonly IMicrochipRepository _chipRepo;

        public AnimalService(IAnimalRepository animalRepo, IMicrochipRepository chipRepo)
        {
            _animalRepo = animalRepo;
            _chipRepo = chipRepo;
        }

        public Animal? GetAnimalByChipNumber(string chipNumber)
        {
            var chip = _chipRepo.GetByChipNumber(chipNumber);
            return chip?.Animal;
        }

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

        // ✅ Added: Used by your unit tests
        public void AddAnimal(Animal animal)
        {
            _animalRepo.Add(animal);
        }

        public List<Animal> GetAllAnimals()
        {
            return _animalRepo.GetAll();
        }
    }
}
