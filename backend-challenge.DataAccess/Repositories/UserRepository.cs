using backend_challenge.DataAccess.Context;
using backend_challenge.DataAccess.Repositories.Interfaces;
using backend_challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend_challenge.DataAccess.Repositories
{
    public class UserRepository : RepositoryAsync<User>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email.Equals(email.ToLower().Trim()));
        }
    }
}
