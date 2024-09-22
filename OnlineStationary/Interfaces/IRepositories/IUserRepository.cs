using OnlineStationary.Models.Domain;

namespace OnlineStationary.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(User user);
        User? GetUserById(Guid id);
        User Get(string email);
        User? GetUser(Func<User, bool> predicate);   
        ICollection<User> GetAllUsers();
        void Save();
    }
}
