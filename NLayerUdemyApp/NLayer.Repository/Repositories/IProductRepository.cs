using NLayer.Core;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetProductsWithCategory();
}