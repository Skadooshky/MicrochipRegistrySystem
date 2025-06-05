using System;
using System.Collections.Generic;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;
using NationalMicrochipRegistry.Data.Enums;
using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Services
{
    /// <summary>
    /// Provides operations for managing animals and assigning them to microchips.
    /// </summary>
    public class AnimalService : IAnimalService
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

        /// <inheritdoc/>
        public void AddAnimal(Animal animal)
        {
            _animalRepo.Add(animal);
        }

        /// <inheritdoc/>
        public List<Animal> GetAllAnimals()
        {
            return _animalRepo.GetAll();
        }

        /// <inheritdoc/>
        public Animal? GetAnimalByChipNumber(string chipNumber)
        {
            var chip = _chipRepo.GetByChipNumber(chipNumber);
            return chip?.Animal;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void UpdateAnimal(Animal animal)
        {
            _animalRepo.Update(animal);
        }
    }
}
