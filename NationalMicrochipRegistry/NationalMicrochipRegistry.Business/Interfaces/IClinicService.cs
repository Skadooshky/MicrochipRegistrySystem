using NationalMicrochipRegistry.Data.Models;
using System.Collections.Generic;

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
        /// <example>
        /// <code>
        /// var clinic = new Clinic
        /// {
        ///     Name = "VetCare Central",
        ///     Address = "123 Main St, Wellington",
        ///     PhoneNumber = "0123456789"
        /// };
        /// clinicService.AddClinic(clinic);
        /// </code>
        /// </example>
        void AddClinic(Clinic clinic);

        /// <summary>
        /// Updates an existing clinic's details.
        /// </summary>
        /// <param name="clinic">The clinic object with updated data.</param>
        /// <example>
        /// <code>
        /// var existingClinic = clinicService.GetClinicById(1);
        /// if (existingClinic != null)
        /// {
        ///     existingClinic.PhoneNumber = "0987654321";
        ///     clinicService.EditClinic(existingClinic);
        /// }
        /// </code>
        /// </example>
        void EditClinic(Clinic clinic);

        /// <summary>
        /// Removes a clinic from the system based on its ID.
        /// </summary>
        /// <param name="clinicId">The unique identifier of the clinic to delete.</param>
        /// <example>
        /// <code>
        /// clinicService.DeleteClinic(3);
        /// </code>
        /// </example>
        void DeleteClinic(int clinicId);

        /// <summary>
        /// Retrieves all registered clinics.
        /// </summary>
        /// <returns>A list of all <see cref="Clinic"/> objects.</returns>
        /// <example>
        /// <code>
        /// var clinics = clinicService.GetAllClinics();
        /// foreach (var clinic in clinics)
        /// {
        ///     Console.WriteLine($"{clinic.Name}, {clinic.Address}");
        /// }
        /// </code>
        /// </example>
        List<Clinic> GetAllClinics();

        /// <summary>
        /// Retrieves a specific clinic by its unique ID.
        /// </summary>
        /// <param name="clinicId">The unique identifier of the clinic to retrieve.</param>
        /// <returns>
        /// The <see cref="Clinic"/> object if found; otherwise, <c>null</c>.
        /// </returns>
        /// <example>
        /// <code>
        /// var clinic = clinicService.GetClinicById(2);
        /// if (clinic != null)
        /// {
        ///     Console.WriteLine($"Clinic: {clinic.Name} at {clinic.Address}");
        /// }
        /// </code>
        /// </example>
        Clinic? GetClinicById(int clinicId);
    }
}
