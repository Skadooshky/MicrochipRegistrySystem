using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Interfaces
{
    public interface IMicrochipService
    {
        void GenerateMicrochips(int count);
        void DeleteMicrochip(string chipNumber);
        Microchip? GetMicrochipByNumber(string chipNumber);
    }
}