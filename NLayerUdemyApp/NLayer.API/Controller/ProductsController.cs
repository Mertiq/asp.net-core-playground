using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.CustomResponseDto;
using NLayer.Core.Service;

namespace NLayer.API.Controller;

public class ProductsController : CustomBaseController
{
    private readonly IMapper _mapper;
    private readonly IService<Product> _service;
    private readonly IProductService _productService;

    public ProductsController(IService<Product> service, IMapper mapper, IProductService productService)
    {
        _service = service;
        _mapper = mapper;
        _productService = productService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetProductsWithCategory()
    {
        return CreateActionResult(await _productService.GetProductsWithCategory());
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var products = await _service.GetAllAsync();
        var productDtos = _mapper.Map<List<ProductDto>>(products.ToList());
        return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        var productDto = _mapper.Map<ProductDto>(product);
        return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
    }

    [HttpPost]
    public async Task<IActionResult> Save(ProductDto productDto)
    {
        var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
        var productsDto = _mapper.Map<ProductDto>(product);
        return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDto productDto)
    {
        await _service.UpdateAsync(_mapper.Map<Product>(productDto));
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpDelete]
    public async Task<IActionResult> Update(int id)
    {
        var product = await _service.GetByIdAsync(id);
        await _service.RemoveAsync(product);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
}