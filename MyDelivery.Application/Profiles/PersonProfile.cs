using AutoMapper;
using MyDelivery.Application.DTOs;
using MyDelivery.Domain.Entities;

namespace MyDelivery.Application.Profiles;

public class PersonProfile : Profile
{
	public PersonProfile()
	{
		CreateMap<Person, PersonDTO>();
		CreateMap<PersonDTO, Person>();

	}
}
