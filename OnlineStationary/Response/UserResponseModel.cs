using OnlineStationary.Models.Domain;

namespace OnlineStationary.Response
{
    public class UserResponseModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        // public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<Role> UserRoles { get; set; } = new List<Role>();
    }

    public class UserRoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;

    }
}
