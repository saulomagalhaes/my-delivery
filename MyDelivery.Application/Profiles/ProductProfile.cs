using AutoMapper;
using MyDelivery.Application.DTOs.Product;
using MyDelivery.Domain.Entities;

namespace MyDelivery.Application.Profiles;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDTO>();
		CreateMap<ProductDTO, Product>();
		CreateMap<Product, ReadProductDTO>();
		CreateMap<ReadProductDTO, Product>();
    }
}
