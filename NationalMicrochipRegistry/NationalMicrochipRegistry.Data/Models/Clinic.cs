using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NationalMicrochipRegistry.Data.Enums;

namespace NationalMicrochipRegistry.Data.Models
{
    public class Clinic
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<Microchip> Microchips { get; set; } = new List<Microchip>();
    }
}
