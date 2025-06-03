using NationalMicrochipRegistry.Data.Enums;
using NationalMicrochipRegistry.Data.Models;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;

namespace NationalMicrochipRegistry.Business.Services
{
    public class MicrochipService : IMicrochipService
    {
        private readonly IMicrochipRepository _chipRepo;

        public MicrochipService(IMicrochipRepository chipRepo)
        {
            _chipRepo = chipRepo;
        }

        public void AddMicrochip(Microchip chip)
        {
            _chipRepo.Add(chip);
        }

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

        public void DeleteMicrochip(string chipNumber) => _chipRepo.DeleteByNumber(chipNumber);

        public Microchip? GetMicrochipByNumber(string chipNumber) => _chipRepo.GetByChipNumber(chipNumber);
    }
}
