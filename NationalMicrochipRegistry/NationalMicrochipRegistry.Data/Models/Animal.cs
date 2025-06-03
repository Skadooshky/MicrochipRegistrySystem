using System.ComponentModel.DataAnnotations;
using NationalMicrochipRegistry.Data.Enums;

namespace NationalMicrochipRegistry.Data.Models
{
    /// <summary>
    /// Represents an animal registered in the microchip registry system.
    /// </summary>
    public class Animal
    {
        /// <summary>
        /// Gets or sets the unique identifier for the animal.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the animal.
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the animal (e.g., Dog, Cat).
        /// </summary>
        [Required]
        public AnimalType Type { get; set; }

        /// <summary>
        /// Gets or sets the breed of the animal (optional).
        /// </summary>
        public string? Breed { get; set; }

        /// <summary>
        /// Gets or sets the sex of the animal (optional).
        /// </summary>
        public AnimalSex? Sex { get; set; }

        /// <summary>
        /// Gets or sets the age of the animal in years (optional).
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// Gets or sets the name of the animal's owner (optional).
        /// </summary>
        [MaxLength(100)]
        public string? OwnerName { get; set; }

        /// <summary>
        /// Gets or sets the contact information of the animal's owner (optional).
        /// </summary>
        [MaxLength(100)]
        public string? OwnerContact { get; set; }

        /// <summary>
        /// Gets or sets the microchip associated with the animal (optional).
        /// </summary>
        public Microchip? Microchip { get; set; }
    }
}
