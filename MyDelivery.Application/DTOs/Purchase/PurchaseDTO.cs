namespace MyDelivery.Application.DTOs.Purchase;

public class PurchaseDTO
{
    public string Code { get; set; }
    public string Document { get; set; }
    public string? ProductName { get; set; }
    public decimal? Price { get; set; }
}
