using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Repositories
{
    public interface IClinicRepository
    {
        void Add(Clinic clinic);
        void Update(Clinic clinic);
        void Delete(int clinicId);
        Clinic? GetById(int clinicId);
        List<Clinic> GetAll();
    }
}
