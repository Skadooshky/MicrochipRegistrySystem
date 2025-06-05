namespace NationalMicrochipRegistry.Data.Enums
{
    /// <summary>
    /// The four possible roles a system user may have.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// A basic user with read‐only access to public data.
        /// </summary>
        User,

        /// <summary>
        /// Registry Administrator: can manage clinics, microchips, and animals.
        /// </summary>
        RegistryAdministrator,

        /// <summary>
        /// System Administrator: can add/remove users and has all other privileges.
        /// </summary>
        SystemAdministrator,

        /// <summary>
        /// Tester: has limited permissions for QA/testing only.
        /// </summary>
        Tester
    }
}
