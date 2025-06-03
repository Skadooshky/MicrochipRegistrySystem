using System.ComponentModel.DataAnnotations;
using NationalMicrochipRegistry.Data.Enums;

namespace NationalMicrochipRegistry.Data.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public AnimalType Type { get; set; }

        public string? Breed { get; set; }

        public AnimalSex? Sex { get; set; }

        public int? Age { get; set; }

        [MaxLength(100)]
        public string? OwnerName { get; set; }

        [MaxLength(100)]
        public string? OwnerContact { get; set; }

        public Microchip? Microchip { get; set; }
    }
}
