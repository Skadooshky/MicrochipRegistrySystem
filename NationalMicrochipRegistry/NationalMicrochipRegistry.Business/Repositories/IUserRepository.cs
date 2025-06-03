using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(int userId);
        User? GetByUsername(string username);
        List<User> GetAll();
    }
}
