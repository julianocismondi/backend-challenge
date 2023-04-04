using backend_challenge.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace backend_challenge.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.CreateAt).IsConcurrencyToken();
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@gmail.com",
                Password = "12345678"
            });
        }
    }
}
