namespace NationalMicrochipRegistry.Data.Enums
{
    /// <summary>
    /// Represents the current status of a microchip in the registry.
    /// </summary>
    public enum ChipStatus
    {
        /// <summary>
        /// The chip is available and not yet assigned to any animal.
        /// </summary>
        Available,

        /// <summary>
        /// The chip is currently assigned to an animal.
        /// </summary>
        Assigned,

        /// <summary>
        /// The chip has been marked as deleted and is no longer in use.
        /// </summary>
        Deleted
    }
}
