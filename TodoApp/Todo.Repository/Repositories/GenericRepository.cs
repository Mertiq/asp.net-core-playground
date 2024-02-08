using Microsoft.EntityFrameworkCore;
using Todo.Core.Repositories;

namespace Todo.Repository.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public IQueryable<T> GetAllAsync()
    {
        return _dbSet.AsNoTracking();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }

    public void DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
    }
}