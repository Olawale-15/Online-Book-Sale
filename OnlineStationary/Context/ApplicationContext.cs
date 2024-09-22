using Microsoft.EntityFrameworkCore;
using OnlineStationary.Models.Domain;

namespace OnlineStationary.Context
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBook> OrderBooks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Wallet> Wallets { get; set; } 
         

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userId = Guid.NewGuid();
            var adminId = Guid.NewGuid();
            var authorId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("password", salt);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
               new User
                {
                   Id = userId,
                   Username = "AbdulQayyum",
                   Email = "admin@gmail.com",
                   Salt = salt,
                   PasswordHash = passwordHash,

                }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = adminId,
                    Name = "Admin"
                }
                );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = userRoleId,
                    UserId = userId,
                    RoleId = adminId,
                }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = customerId,
                    Name = "Customer",
                }
                );

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = authorId,
                    Name = "Author"
                }
                );

        }
    }
   
}
