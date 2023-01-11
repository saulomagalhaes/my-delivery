using AutoMapper;
using MyDelivery.Application.DTOs;
using MyDelivery.Application.DTOs.Validations;
using MyDelivery.Application.Services.Contracts;
using MyDelivery.Domain.Contracts.Repositories;
using MyDelivery.Domain.Entities;

namespace MyDelivery.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<ResultService<PersonDTO>> Create(PersonDTO personDTO)
    {
        if (personDTO == null)
            return ResultService.Fail<PersonDTO>("O objeto deve ser informado");
        
        var result = new PersonDTOValidator().Validate(personDTO);
        if (!result.IsValid)
            return ResultService.RequestError<PersonDTO>("Requisição falhou na validação", result);

        var person = _mapper.Map<Person>(personDTO);
        var data = await _personRepository.Create(person);
        return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(data));
    }
}
