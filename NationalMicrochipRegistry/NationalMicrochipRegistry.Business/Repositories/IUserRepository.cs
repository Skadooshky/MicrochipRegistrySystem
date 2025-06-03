using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Repositories
{
    /// <summary>
    /// Provides methods for managing user records in the data store.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a new user to the data store.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object to add.</param>
        void Add(User user);

        /// <summary>
        /// Deletes a user from the data store by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique ID of the user to delete.</param>
        void Delete(int userId);

        /// <summary>
        /// Retrieves a user based on their username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>The <see cref="User"/> if found; otherwise, <c>null</c>.</returns>
        User? GetByUsername(string username);

        /// <summary>
        /// Retrieves all users from the data store.
        /// </summary>
        /// <returns>A list of all <see cref="User"/> records.</returns>
        List<User> GetAll();
    }
}
