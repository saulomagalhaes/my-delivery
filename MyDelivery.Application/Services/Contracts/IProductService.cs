using MyDelivery.Application.DTOs.Product;

namespace MyDelivery.Application.Services.Contracts;

public interface IProductService
{
    Task<ResultService<ProductDTO>> Create(ProductDTO productDTO);
    Task<ResultService<ICollection<ReadProductDTO>>> GetProducts(int page, int rows);
    Task<ResultService<ReadProductDTO>> GetById(int id);
    Task<ResultService> Update(int id, ProductDTO productDTO);
    Task<ResultService> Delete(int id);
}
