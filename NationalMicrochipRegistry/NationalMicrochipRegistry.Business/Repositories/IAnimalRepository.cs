using NationalMicrochipRegistry.Data.Models;
using System.Collections.Generic;

namespace NationalMicrochipRegistry.Business.Repositories
{
    public interface IAnimalRepository
    {
        void Add(Animal animal);
        List<Animal> GetAll();
        Animal? GetById(int id);
        void Update(Animal animal);
        void Delete(int id);
    }
}
