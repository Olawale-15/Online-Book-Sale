using Microsoft.EntityFrameworkCore;
using OnlineStationary.Context;
using OnlineStationary.Interfaces.IRepositories;
using OnlineStationary.Models.Domain;
using System.Linq;

namespace OnlineStationary.Implementations.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationContext _context;
        public RoleRepository(ApplicationContext context)
        {
            _context = context; 
        }
        public Role CreateRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public bool DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }

        public UserRole GetURole(Func<UserRole, bool> predicate)
        {
           var getRole = _context.UserRoles
                .Include(r=> r.Role)
                .Include(r => r.User)
                .FirstOrDefault(predicate);  // _context.Roles.FirstOrDefault(predicate);
            return getRole;
        }

        public Role? GetRoleById(Guid id)
        {
            var getRole = _context.Roles.FirstOrDefault(x => x.Id == id);
            return getRole;
        }

        public Role UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return role;
        }

        public void Save()
        {
            var r = _context.SaveChanges();
           
        }

        public Role GetRole(Func<Role, bool> predicate)
        {
            var getRole = _context.Roles
               .Include(r => r.UserRoles)
               .FirstOrDefault(predicate);  // _context.Roles.FirstOrDefault(predicate);
            return getRole;
        }
    }
}
