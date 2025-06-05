using System;
using System.Collections.Generic;
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

        public MicrochipService(IMicrochipRepository chipRepo)
        {
            _chipRepo = chipRepo;
        }

        /// <inheritdoc/>
        public void AddMicrochip(Microchip chip)
        {
            _chipRepo.Add(chip);
        }

        /// <inheritdoc/>
        public void GenerateMicrochips(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newChip = new Microchip
                {
                    ChipNumber = Guid.NewGuid().ToString(),
                    Status = ChipStatus.Available
                };
                _chipRepo.Add(newChip);
            }
        }

        /// <inheritdoc/>
        public void DeleteMicrochip(string chipNumber) =>
            _chipRepo.DeleteByNumber(chipNumber);

        /// <inheritdoc/>
        public Microchip? GetMicrochipByNumber(string chipNumber) =>
            _chipRepo.GetByChipNumber(chipNumber);

        /// <inheritdoc/>
        public List<Microchip> GetAllMicrochips() =>
            _chipRepo.GetAll();

        /// <inheritdoc/>
        public Microchip? GetMicrochipByAnimalId(int animalId) =>
            _chipRepo.GetByAnimalId(animalId);

        /// <inheritdoc/>
        public void UpdateMicrochip(Microchip chip) =>
            _chipRepo.Update(chip);
    }
}
