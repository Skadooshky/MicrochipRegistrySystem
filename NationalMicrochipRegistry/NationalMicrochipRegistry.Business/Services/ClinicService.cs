using NationalMicrochipRegistry.Data.Models;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;

namespace NationalMicrochipRegistry.Business.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepo;

        public ClinicService(IClinicRepository clinicRepo)
        {
            _clinicRepo = clinicRepo;
        }

        public void AddClinic(Clinic clinic) => _clinicRepo.Add(clinic);

        public void EditClinic(Clinic clinic) => _clinicRepo.Update(clinic);

        public void DeleteClinic(int clinicId) => _clinicRepo.Delete(clinicId);

        public List<Clinic> GetAllClinics() => _clinicRepo.GetAll();

        public Clinic? GetClinicById(int clinicId) => _clinicRepo.GetById(clinicId);
    }
}