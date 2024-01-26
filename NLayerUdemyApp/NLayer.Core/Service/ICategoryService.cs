using NLayer.Core.DTOs;
using NLayer.Core.DTOs.CustomResponseDto;

namespace NLayer.Core.Service;

public interface ICategoryService : IService<Category>
{
    public Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductAsync(int categoryId);
}