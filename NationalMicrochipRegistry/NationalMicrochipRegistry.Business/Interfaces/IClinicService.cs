using System.Collections.Generic;
using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Interfaces
{
    /// <summary>
    /// Defines operations related to managing clinic entities.
    /// </summary>
    public interface IClinicService
    {
        /// <summary>
        /// Adds a new clinic to the system.
        /// </summary>
        /// <param name="clinic">The clinic object to add.</param>
        void AddClinic(Clinic clinic);

        /// <summary>
        /// Updates an existing clinic's details.
        /// </summary>
        /// <param name="clinic">The clinic object with updated data.</param>
        void EditClinic(Clinic clinic);

        /// <summary>
        /// Removes a clinic from the system based on its ID.
        /// </summary>
        /// <param name="clinicId">The unique identifier of the clinic to delete.</param>
        void DeleteClinic(int clinicId);

        /// <summary>
        /// Retrieves all registered clinics.
        /// </summary>
        /// <returns>A list of all <see cref="Clinic"/> objects.</returns>
        List<Clinic> GetAllClinics();

        /// <summary>
        /// Retrieves a specific clinic by its unique ID.
        /// </summary>
        /// <param name="clinicId">The unique identifier of the clinic to retrieve.</param>
        /// <returns>
        /// The <see cref="Clinic"/> object if found; otherwise, <c>null</c>.
        /// </returns>
        Clinic? GetClinicById(int clinicId);
    }
}
