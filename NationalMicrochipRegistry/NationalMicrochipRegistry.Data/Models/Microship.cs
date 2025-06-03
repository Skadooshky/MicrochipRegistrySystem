using System.ComponentModel.DataAnnotations;
using NationalMicrochipRegistry.Data.Enums;

namespace NationalMicrochipRegistry.Data.Models
{
    /// <summary>
    /// Represents a microchip that can be assigned to an animal and managed by a clinic.
    /// </summary>
    public class Microchip
    {
        /// <summary>
        /// Gets or sets the unique identifier for the microchip.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique chip number for this microchip.
        /// </summary>
        [Required, MaxLength(20)]
        public string ChipNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the current status of the microchip (e.g., Available, Assigned, Deleted).
        /// </summary>
        [Required]
        public ChipStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the ID of the clinic that holds the microchip.
        /// </summary>
        public int? ClinicId { get; set; }

        /// <summary>
        /// Gets or sets the clinic entity associated with this microchip.
        /// </summary>
        public Clinic? Clinic { get; set; }

        /// <summary>
        /// Gets or sets the ID of the animal to which the microchip is assigned.
        /// </summary>
        public int? AnimalId { get; set; }

        /// <summary>
        /// Gets or sets the animal entity associated with this microchip.
        /// </summary>
        public Animal? Animal { get; set; }
    }
}
