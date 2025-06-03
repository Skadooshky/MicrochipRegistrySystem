using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using NationalMicrochipRegistry.Data.Enums;

namespace NationalMicrochipRegistry.Data.Models
{
    public class Microchip
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string ChipNumber { get; set; } = string.Empty;

        [Required]
        public ChipStatus Status { get; set; }

        public int? ClinicId { get; set; }
        public Clinic? Clinic { get; set; }

        public int? AnimalId { get; set; }
        public Animal? Animal { get; set; }
    }
}
