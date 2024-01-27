using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.CustomResponseDto;
using NLayer.Core.Service;
using NLayer.Core.UnitOfWork;
using NLayer.Repository.Repositories;
using NLayer.Service.Exceptions;

namespace NLayer.Caching;

public class ProductServiceWithCaching : IProductService
{
    private const string CacheProductKey = "productsCache";
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
        _repository = repository;
        _unitOfWork = unitOfWork;

        if (!_memoryCache.TryGetValue(CacheProductKey, out _))
        {
            _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
        }
    }

    public Task<Product> GetByIdAsync(int id)
    {
        var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            throw new NotFoundException($"{typeof(Product).Name}({id}) not found");
        }

        return Task.FromResult(product);
    }

    public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
    }

    public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
    {
        return _memoryCache.Get<IEnumerable<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProducts();
        return entity;
    }

    public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProducts();
        return entities;
    }

    public async Task RemoveAsync(Product entity)
    {
        _repository.Remove(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProducts();
    }

    public async Task RemoveRangeAsync(IEnumerable<Product> entities)
    {
        _repository.RemoveRange(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProducts();
    }

    public async Task UpdateAsync(Product entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProducts();
    }

    public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
    {
        var product = _memoryCache.Get<List<Product>>(CacheProductKey);
        var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
        return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsWithCategoryDto));
    }

    public async Task CacheAllProducts()
    {
        await _memoryCache.Set(CacheProductKey, _repository.GetAll().ToListAsync());
    }
}