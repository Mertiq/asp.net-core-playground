using System.Linq.Expressions;

namespace NLayer.Core.Service;

public interface IService<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    Task RemoveAsync(T entity);
    Task RemoveRangeAsync(IEnumerable<T> entities);
    
    Task UpdateAsync(T entity);
}