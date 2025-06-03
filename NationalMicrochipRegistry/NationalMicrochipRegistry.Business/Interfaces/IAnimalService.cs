using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Interfaces
{
    public interface IAnimalService
    {
        Animal? GetAnimalByChipNumber(string chipNumber);
        void AssignAnimalToChip(string chipNumber, Animal animal);
    }
}