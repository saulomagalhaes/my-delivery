using MyDelivery.Application.DTOs.Product;
using MyDelivery.Application.DTOs.Purchase;

namespace MyDelivery.Application.Services.Contracts;

public interface IPurchaseService
{
    Task<ResultService<ReadPurchaseDTO>> Create(PurchaseDTO purchaseDTO);
    Task<ResultService<ICollection<PurchaseDetailsDTO>>> GetPurchases(int page, int rows);
    Task<ResultService<PurchaseDetailsDTO>> GetById(int id);
    Task<ResultService> Update(int id, PurchaseDTO purchaseDTO);
    Task<ResultService> Delete(int id);
}
