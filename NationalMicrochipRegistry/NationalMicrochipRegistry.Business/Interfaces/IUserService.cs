using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Interfaces
{
    public interface IUserService
    {
        void AddUser(User user);
        void RemoveUser(int userId);
        bool Authenticate(string username, string password);
        List<User> GetAllUsers();
    }
}