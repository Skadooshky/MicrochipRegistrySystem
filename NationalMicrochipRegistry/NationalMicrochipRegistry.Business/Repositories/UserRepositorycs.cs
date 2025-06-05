using System.Collections.Generic;
using System.Linq;
using NationalMicrochipRegistry.Data;
using NationalMicrochipRegistry.Data.Models;

namespace NationalMicrochipRegistry.Business.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RegistryDbContext _context;

        public UserRepository()
        {
            // Assuming you have a parameterless constructor for simplicity:
            _context = new RegistryDbContextFactory().CreateDbContext(new string[0]);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User? GetByUsername(string username)
        {
            return _context.Users
                           .FirstOrDefault(u => u.Username == username);
        }

        public User? GetById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }
    }
}
