using MyDelivery.Application.DTOs.Purchase;

namespace MyDelivery.Application.Services.Contracts;

public interface IPurchaseService
{
    Task<ResultService<ReadPurchaseDTO>> Create(PurchaseDTO purchaseDTO);
}
