using Microsoft.EntityFrameworkCore;
using NLayer.Core;

namespace NLayer.Repository.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetProductsWithCategory()
    {
        return await Context.Products.Include(x => x.Category).ToListAsync();
    }
}