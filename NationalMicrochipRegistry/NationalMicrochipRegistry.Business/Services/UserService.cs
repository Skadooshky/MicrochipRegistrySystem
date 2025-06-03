using NationalMicrochipRegistry.Data.Models;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;

namespace NationalMicrochipRegistry.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public void AddUser(User user) => _userRepo.Add(user);

        public void RemoveUser(int userId) => _userRepo.Delete(userId);

        public bool Authenticate(string username, string password)
        {
            var user = _userRepo.GetByUsername(username);
            return user != null && user.PasswordHash == password;
        }

        public List<User> GetAllUsers() => _userRepo.GetAll();
    }
}