using NationalMicrochipRegistry.Data.Enums;

namespace NationalMicrochipRegistry.App.Helpers
{
    /// <summary>
    /// Centralized permission checks based on UserRole.
    /// </summary>
    public static class PermissionHelper
    {
        /// <summary>
        /// Returns true if a user with the given role may manage clinics.
        /// RegistryAdministrator, SystemAdministrator, or Tester.
        /// </summary>
        public static bool CanManageClinics(UserRole role) =>
            role == UserRole.RegistryAdministrator
         || role == UserRole.SystemAdministrator
         || role == UserRole.Tester;

        /// <summary>
        /// Returns true if a user with the given role may generate microchips.
        /// RegistryAdministrator, SystemAdministrator, or Tester.
        /// </summary>
        public static bool CanGenerateMicrochips(UserRole role) =>
            role == UserRole.RegistryAdministrator
         || role == UserRole.SystemAdministrator
         || role == UserRole.Tester;

        /// <summary>
        /// Returns true if a user with the given role may assign chips to animals.
        /// RegistryAdministrator, SystemAdministrator, or Tester.
        /// </summary>
        public static bool CanAssignChips(UserRole role) =>
            role == UserRole.RegistryAdministrator
         || role == UserRole.SystemAdministrator
         || role == UserRole.Tester;

        /// <summary>
        /// Returns true if a user with the given role may delete chips.
        /// RegistryAdministrator, SystemAdministrator, or Tester.
        /// </summary>
        public static bool CanDeleteChips(UserRole role) =>
            role == UserRole.RegistryAdministrator
         || role == UserRole.SystemAdministrator
         || role == UserRole.Tester;

        /// <summary>
        /// Returns true if a user with the given role may manage animals (add/edit/delete).
        /// RegistryAdministrator, SystemAdministrator, or Tester.
        /// </summary>
        public static bool CanManageAnimals(UserRole role) =>
            role == UserRole.RegistryAdministrator
         || role == UserRole.SystemAdministrator
         || role == UserRole.Tester;

        /// <summary>
        /// Returns true if a user with the given role may manage users.
        /// SystemAdministrator or Tester.
        /// </summary>
        public static bool CanManageUsers(UserRole role) =>
            role == UserRole.SystemAdministrator
         || role == UserRole.Tester;

        /// <summary>
        /// Returns true if a user with the given role may test features.
        /// Tester (but since Tester is “zero restrictions,” 
        /// this returns true as well).
        /// </summary>
        public static bool CanTestFeatures(UserRole role) =>
            role == UserRole.Tester
         || role == UserRole.RegistryAdministrator
         || role == UserRole.SystemAdministrator;
    }
}
