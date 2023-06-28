using backend_challenge.DataAccess.Context;
using backend_challenge.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_challenge.DataAccess.Repositories
{
    public abstract class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public RepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await Save();
        }

        public async Task Delete(int id)
        {
            T entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await Save();
            }
            else
            {
                throw new HttpRequestException("No se encontró el usuario");
            }
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await Save();
        }
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
