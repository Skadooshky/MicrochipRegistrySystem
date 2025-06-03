using NationalMicrochipRegistry.Data.Models;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;

namespace NationalMicrochipRegistry.Business.Services
{
    /// <summary>
    /// Provides user management and authentication services.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepo">The user repository to interact with the data store.</param>
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <example>
        /// <code>
        /// var newUser = new User
        /// {
        ///     Username = "admin",
        ///     PasswordHash = "hashedpassword",
        ///     Role = "Administrator"
        /// };
        /// userService.AddUser(newUser);
        /// </code>
        /// </example>
        public void AddUser(User user) => _userRepo.Add(user);

        /// <summary>
        /// Removes a user from the system by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to remove.</param>
        /// <example>
        /// <code>
        /// userService.RemoveUser(3);
        /// </code>
        /// </example>
        public void RemoveUser(int userId) => _userRepo.Delete(userId);

        /// <summary>
        /// Authenticates a user by verifying their username and password.
        /// </summary>
        /// <param name="username">The username provided during login.</param>
        /// <param name="password">The password provided during login.</param>
        /// <returns><c>true</c> if authentication is successful; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// bool isAuthenticated = userService.Authenticate("admin", "hashedpassword");
        /// if (isAuthenticated)
        /// {
        ///     Console.WriteLine("Login successful.");
        /// }
        /// else
        /// {
        ///     Console.WriteLine("Invalid credentials.");
        /// }
        /// </code>
        /// </example>
        public bool Authenticate(string username, string password)
        {
            var user = _userRepo.GetByUsername(username);
            return user != null && user.PasswordHash == password;
        }

        /// <summary>
        /// Retrieves a list of all registered users.
        /// </summary>
        /// <returns>A list of <see cref="User"/> objects.</returns>
        /// <example>
        /// <code>
        /// var users = userService.GetAllUsers();
        /// foreach (var user in users)
        /// {
        ///     Console.WriteLine($"{user.Username} - {user.Role}");
        /// }
        /// </code>
        /// </example>
        public List<User> GetAllUsers() => _userRepo.GetAll();
    }
}
