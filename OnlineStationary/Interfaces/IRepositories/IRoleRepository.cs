using OnlineStationary.Models.Domain;

namespace OnlineStationary.Interfaces.IRepositories
{
    public interface IRoleRepository
    {
        Role CreateRole(Role role);
        Role UpdateRole(Role role);
        bool DeleteRole(Role role);
        Role? GetRoleById(Guid id);
        UserRole GetURole(Func<UserRole, bool> predicate);
        Role? GetRole(Func<Role, bool> predicate);
        void Save();
    }
}
