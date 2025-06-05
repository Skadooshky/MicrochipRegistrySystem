using System.ComponentModel.DataAnnotations;
using NationalMicrochipRegistry.Data.Enums;

namespace NationalMicrochipRegistry.Data.Models
{
    /// <summary>
    /// Represents a system user who can log in and perform actions based on their role.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username used for login.
        /// </summary>
        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hashed password used for authentication.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role of the user.
        /// Must be one of the values in <see cref="UserRole"/>.
        /// </summary>
        [Required]
        public UserRole Role { get; set; }
    }
}
