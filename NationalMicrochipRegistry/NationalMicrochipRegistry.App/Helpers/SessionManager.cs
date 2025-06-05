using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.App.Helpers
{
    /// <summary>
    /// Simple in‐memory session storage for the currently authenticated user.
    /// </summary>
    public static class SessionManager
    {
        /// <summary>
        /// The currently logged‐in user (null if no one is signed in).
        /// </summary>
        public static User? CurrentUser { get; set; }
    }
}
