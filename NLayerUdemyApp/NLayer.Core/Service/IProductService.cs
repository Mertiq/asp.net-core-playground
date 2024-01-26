using NLayer.Core.DTOs;
using NLayer.Core.DTOs.CustomResponseDto;

namespace NLayer.Core.Service;

public interface IProductService : IService<Product>
{
    Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();
}