using backend_challenge.Helpers;
using backend_challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_challenge.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Admin" }, new Role { Id = 2, Name = "User" });

            modelBuilder.Entity<User>().HasData(new User { Id = 1, Email = "admin@gmail.com", Name = "Admin", Password = HashPass.HashPassword("123456"), RoleId = 1 });
        }
    }
}
