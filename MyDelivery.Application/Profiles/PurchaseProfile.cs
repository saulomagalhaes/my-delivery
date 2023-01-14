using AutoMapper;
using MyDelivery.Application.DTOs.Purchase;
using MyDelivery.Domain.Entities;

namespace MyDelivery.Application.Profiles;

public class PurchaseProfile : Profile
{
	public PurchaseProfile()
	{
		CreateMap<Purchase, PurchaseDTO>().ReverseMap();
		CreateMap<Purchase, ReadPurchaseDTO>().ReverseMap();
		CreateMap<ReadPurchaseDTO, PurchaseDTO>().ReverseMap();
    }
}
