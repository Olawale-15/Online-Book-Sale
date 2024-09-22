using Microsoft.EntityFrameworkCore;
using OnlineStationary.Context;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Models.Domain;

namespace OnlineStationary.Implementations.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public ICollection<User> GetAllUsers()
        {
            var getAllUsers = _context.Users.ToList();
            return getAllUsers;
        }

        public User GetUser(Func<User, bool> predicate)
        {
            var getUser = _context.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role)
                //.Include(u  => u.UserRoles)
                //.ThenInclude(u => u.Role)
                .FirstOrDefault(predicate);
            return getUser;
        }

        public User Get(string email)
        {
            var user = _context.Users
                .Include(r => r.UserRoles)
                .ThenInclude(e => e.Role)
                .FirstOrDefault(s => s.Email == email);
            return user;
        }

        public User? GetUserById(Guid id)
        {
            var getUser = _context.Users.FirstOrDefault( x=> x.Id == id);
            return getUser;
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        
    }
}
