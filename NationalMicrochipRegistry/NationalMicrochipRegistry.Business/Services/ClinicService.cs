using System.Collections.Generic;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;
using NationalMicrochipRegistry.Data.Models;

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

        /// <inheritdoc/>
        public void AddClinic(Clinic clinic) => _clinicRepo.Add(clinic);

        /// <inheritdoc/>
        public void EditClinic(Clinic clinic) => _clinicRepo.Update(clinic);

        /// <inheritdoc/>
        public void DeleteClinic(int clinicId) => _clinicRepo.Delete(clinicId);

        /// <inheritdoc/>
        public List<Clinic> GetAllClinics() => _clinicRepo.GetAll();

        /// <inheritdoc/>
        public Clinic? GetClinicById(int clinicId) => _clinicRepo.GetById(clinicId);
    }
}
