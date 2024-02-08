using Microsoft.EntityFrameworkCore;
using Todo.Core.Repositories;
using Todo.Core.Services;
using Todo.Core.UnitOfWorks;

namespace Todo.Service;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IGenericRepository<T> _genericRepository;

    private readonly IUnitOfWork _unitOfWork;

    public GenericService(IGenericRepository<T> genericRepository, IUnitOfWork unitOfWork)
    {
        _genericRepository = genericRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _genericRepository.GetByIdAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _genericRepository.GetAllAsync().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _genericRepository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _genericRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    public Task DeleteAsync(T entity)
    {
        _genericRepository.DeleteAsync(entity);
        return _unitOfWork.CommitAsync();
    }
}