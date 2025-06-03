using NationalMicrochipRegistry.Data.Models;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;

namespace NationalMicrochipRegistry.Business.Services
{
    /// <summary>
    /// Provides services for managing clinic data.
    /// </summary>
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicService"/> class.
        /// </summary>
        /// <param name="clinicRepo">The clinic repository.</param>
        public ClinicService(IClinicRepository clinicRepo)
        {
            _clinicRepo = clinicRepo;
        }

        /// <summary>
        /// Adds a new clinic to the repository.
        /// </summary>
        /// <param name="clinic">The clinic to add.</param>
        public void AddClinic(Clinic clinic) => _clinicRepo.Add(clinic);

        /// <summary>
        /// Updates the information for an existing clinic.
        /// </summary>
        /// <param name="clinic">The updated clinic object.</param>
        public void EditClinic(Clinic clinic) => _clinicRepo.Update(clinic);

        /// <summary>
        /// Deletes a clinic from the repository by its ID.
        /// </summary>
        /// <param name="clinicId">The ID of the clinic to delete.</param>
        public void DeleteClinic(int clinicId) => _clinicRepo.Delete(clinicId);

        /// <summary>
        /// Retrieves all clinics from the repository.
        /// </summary>
        /// <returns>A list of all <see cref="Clinic"/> entries.</returns>
        public List<Clinic> GetAllClinics() => _clinicRepo.GetAll();

        /// <summary>
        /// Retrieves a clinic by its ID.
        /// </summary>
        /// <param name="clinicId">The ID of the clinic to retrieve.</param>
        /// <returns>The <see cref="Clinic"/> if found; otherwise, <c>null</c>.</returns>
        public Clinic? GetClinicById(int clinicId) => _clinicRepo.GetById(clinicId);
    }
}
