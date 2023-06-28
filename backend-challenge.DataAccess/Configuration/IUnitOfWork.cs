using backend_challenge.DataAccess.Repositories.Interfaces;

namespace backend_challenge.DataAccess.Configuration
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        int Save();
    } 
}
