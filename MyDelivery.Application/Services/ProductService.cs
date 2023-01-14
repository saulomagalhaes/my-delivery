using AutoMapper;
using MyDelivery.Application.DTOs.Product;
using MyDelivery.Application.DTOs.Validations;
using MyDelivery.Application.Services.Contracts;
using MyDelivery.Domain.Contracts.Repositories;
using MyDelivery.Domain.Entities;

namespace MyDelivery.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ResultService<ProductDTO>> Create(ProductDTO productDTO)
    {
        var result = new ProductDTOValidator().Validate(productDTO);
        if (!result.IsValid)
            return ResultService.RequestError<ProductDTO>("Problema na validação", result);
        
        var product = _mapper.Map<Product>(productDTO);
        var data = await _productRepository.Create(product);
        return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(data), data.Id);

    }

    public async Task<ResultService> Delete(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null)
            return ResultService.Fail<ReadProductDTO>("Produto não encontrado");
        await _productRepository.Delete(product);
        return ResultService.Ok("NoContent");
    }

    public async Task<ResultService<ReadProductDTO>> GetById(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null)
            return ResultService.Fail<ReadProductDTO>("Produto não encontrado");
        return ResultService.Ok<ReadProductDTO>(_mapper.Map<ReadProductDTO>(product));
    }

    public async Task<ResultService<ICollection<ReadProductDTO>>> GetProducts()
    {
        var products = await _productRepository.GetProducts();
        return ResultService.Ok<ICollection<ReadProductDTO>>(_mapper.Map<ICollection<ReadProductDTO>>(products));
    }

    public async Task<ResultService> Update(int id, ProductDTO productDTO)
    {
        var validation = new ProductDTOValidator().Validate(productDTO);
        if (!validation.IsValid)
            return ResultService.RequestError("Problema na validação", validation);

        var product = await _productRepository.GetById(id);
        if (product == null)
            return ResultService.Fail("Produto não encontrado");

        product = _mapper.Map(productDTO, product);
        await _productRepository.Update(product);
        return ResultService.Ok("NoContent");
    }
}
