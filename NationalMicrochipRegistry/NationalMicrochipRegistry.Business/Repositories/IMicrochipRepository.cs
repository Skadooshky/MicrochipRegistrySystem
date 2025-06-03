using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Repositories
{
    public interface IMicrochipRepository
    {
        void Add(Microchip chip);
        void Update(Microchip chip);
        void DeleteByNumber(string chipNumber);
        Microchip? GetByChipNumber(string chipNumber);
    }
}