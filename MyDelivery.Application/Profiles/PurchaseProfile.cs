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
		CreateMap<Purchase, PurchaseDetailsDTO>()
			.ForMember(x => x.Person, opt => opt.Ignore())
			.ForMember(x => x.Product, opt => opt.Ignore())
			.ConstructUsing((model, context) =>
			{
				var dto = new PurchaseDetailsDTO
				{
					Id = model.Id,
					Person = model.Person.Name,
					Product = model.Product.Name,
					Date = model.Date
				};
				return dto;
			});
    }
}
