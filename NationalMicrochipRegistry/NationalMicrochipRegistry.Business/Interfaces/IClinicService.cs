using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Interfaces
{
    public interface IClinicService
    {
        void AddClinic(Clinic clinic);
        void EditClinic(Clinic clinic);
        void DeleteClinic(int clinicId);
        List<Clinic> GetAllClinics();
        Clinic? GetClinicById(int clinicId);
    }
}