namespace Todo.Core.Repositories;

public interface IGenericRepository <T> where T : class
{
    Task<T> GetByIdAsync(int id);
    IEnumerable<T> GetAllAsync();
    Task<T> AddAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);
}