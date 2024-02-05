using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext Context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        Context = context;
        _dbSet = Context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet.AsNoTracking().AsQueryable();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where(expression);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}