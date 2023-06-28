using backend_challenge.Domain.Entities;

namespace backend_challenge.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryAsync<User>
    {
        Task<IList<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
    }
}
