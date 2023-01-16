using MyDelivery.Application.DTOs.Purchase;

namespace MyDelivery.Application.Services.Contracts;

public interface IPurchaseService
{
    Task<ResultService<ReadPurchaseDTO>> Create(PurchaseDTO purchaseDTO);
    Task<ResultService<ICollection<PurchaseDetailsDTO>>> GetPurchases();
    Task<ResultService<PurchaseDetailsDTO>> GetById(int id);
}
