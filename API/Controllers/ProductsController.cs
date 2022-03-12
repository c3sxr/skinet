using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRep;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<ProductType> _productTypeRepo;

    public ProductsController(IGenericRepository<Product> productsRepo,
                              IGenericRepository<ProductBrand> productBrandRepo,
                              IGenericRepository<ProductType> productTypeRep,
                              IMapper mapper)
    {
        _productsRepo = productsRepo;
        _productBrandRepo = productBrandRepo;
        _productTypeRep = productTypeRep;
        _mapper = mapper;
        _productTypeRepo = productTypeRep;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();

        var products = await _productsRepo.ListAsync(spec);

        if (products is not null){
            return Ok(_mapper.
                Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));

        }

        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _productsRepo.GetEntityWithSpec(spec);

        if (product == null) return NotFound(new ApiResponse(404));

        return _mapper.Map<Product,ProductToReturnDto>(product);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _productBrandRepo.ListAllAsync());
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await _productTypeRepo.ListAllAsync());
    }
}
