namespace backend_challenge.DataAccess.Repositories.Interfaces
{
   public interface IRepositoryAsync<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task Create(T entity);
        Task Delete(int id);
        Task Update(T entity);
    }
}
