using System.Collections.Generic;
using NationalMicrochipRegistry.Data.Models;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;
using NationalMicrochipRegistry.Data.Enums;

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

        /// <inheritdoc/>
        public void AddUser(User user) => _userRepo.Add(user);

        /// <inheritdoc/>
        public void RemoveUser(int userId) => _userRepo.Delete(userId);

        /// <inheritdoc/>
        public User? Authenticate(string username, string password)
        {
            var user = _userRepo.GetByUsername(username);
            if (user != null && user.PasswordHash == password)
            {
                return user;
            }
            return null;
        }

        /// <inheritdoc/>
        public List<User> GetAllUsers() => _userRepo.GetAll();

        /// <inheritdoc/>
        public bool UserHasRole(int userId, UserRole role)
        {
            var user = _userRepo.GetById(userId);
            return user != null && user.Role == role;
        }
    }
}
