using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Interfaces
{
    /// <summary>
    /// Defines methods for managing users and authentication within the system.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object containing user details.</param>
        void AddUser(User user);

        /// <summary>
        /// Removes a user from the system based on their unique ID.
        /// </summary>
        /// <param name="userId">The ID of the user to remove.</param>
        void RemoveUser(int userId);

        /// <summary>
        /// Authenticates a user using their username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password provided for authentication.</param>
        /// <returns><c>true</c> if authentication is successful; otherwise, <c>false</c>.</returns>
        bool Authenticate(string username, string password);

        /// <summary>
        /// Retrieves a list of all users currently in the system.
        /// </summary>
        /// <returns>A list of <see cref="User"/> objects.</returns>
        List<User> GetAllUsers();
    }
}
