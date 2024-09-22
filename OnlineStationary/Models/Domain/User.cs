using System.Security.Cryptography.X509Certificates;

namespace OnlineStationary.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Salt { get; set; } = default!;
        public Wallet Wallet { get; set; } = default!;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
