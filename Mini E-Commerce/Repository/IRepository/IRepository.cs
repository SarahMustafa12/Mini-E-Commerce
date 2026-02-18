using System.Linq.Expressions;

namespace Mini_E_Commerce.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>> GetAsync(Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>[]? includes = null,
            bool tracked = true);
        Task CommitAsync();
        Task<T> UpdateAsync(T entity);
        Task <T?> GetOneAsync(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true);
      
    }
}
