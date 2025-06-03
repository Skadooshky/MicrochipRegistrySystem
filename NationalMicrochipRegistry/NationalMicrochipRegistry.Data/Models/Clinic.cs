using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NationalMicrochipRegistry.Data.Enums;

namespace NationalMicrochipRegistry.Data.Models
{
    /// <summary>
    /// Represents a clinic that manages and implants microchips.
    /// </summary>
    public class Clinic
    {
        /// <summary>
        /// Gets or sets the unique identifier for the clinic.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the clinic.
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the physical address of the clinic.
        /// </summary>
        [Required, MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the contact phone number of the clinic.
        /// </summary>
        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of microchips assigned to the clinic.
        /// </summary>
        public ICollection<Microchip> Microchips { get; set; } = new List<Microchip>();
    }
}
