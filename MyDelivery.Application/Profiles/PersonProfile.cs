using AutoMapper;
using MyDelivery.Application.DTOs.Person;
using MyDelivery.Domain.Entities;

namespace MyDelivery.Application.Profiles;

public class PersonProfile : Profile
{
	public PersonProfile()
	{
		CreateMap<Person, ReadPersonDTO>();
		CreateMap<ReadPersonDTO, Person>();
        CreateMap<Person, PersonDTO>();
        CreateMap<PersonDTO, Person>();
    }
}
