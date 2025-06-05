using System.Collections.Generic;
using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Repositories
{
    /// <summary>
    /// Defines data operations for managing clinic records.
    /// </summary>
    public interface IClinicRepository
    {
        /// <summary>
        /// Adds a new clinic to the data store.
        /// </summary>
        /// <param name="clinic">The <see cref="Clinic"/> object to add.</param>
        void Add(Clinic clinic);

        /// <summary>
        /// Updates an existing clinic record.
        /// </summary>
        /// <param name="clinic">The <see cref="Clinic"/> object containing updated information.</param>
        void Update(Clinic clinic);

        /// <summary>
        /// Deletes a clinic from the data store by its ID.
        /// </summary>
        /// <param name="clinicId">The unique identifier of the clinic to delete.</param>
        void Delete(int clinicId);

        /// <summary>
        /// Retrieves a clinic by its unique identifier.
        /// </summary>
        /// <param name="clinicId">The ID of the clinic to retrieve.</param>
        /// <returns>The corresponding <see cref="Clinic"/> if found; otherwise, <c>null</c>.</returns>
        Clinic? GetById(int clinicId);

        /// <summary>
        /// Retrieves all clinics from the data store.
        /// </summary>
        /// <returns>A list of all <see cref="Clinic"/> records.</returns>
        List<Clinic> GetAll();
    }
}
