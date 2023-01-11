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

    public async Task<ResultService<PersonDTO>> GetById(int id)
    {
        var person = await _personRepository.GetById(id);
        if(person == null)
            return ResultService.Fail<PersonDTO>("Pessoa não encontrado");
        return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(person));
    }

    public async Task<ResultService<ICollection<PersonDTO>>> GetPeople()
    {
        var people = await _personRepository.GetPeople();
        return ResultService.Ok<ICollection<PersonDTO>>(_mapper.Map<ICollection<PersonDTO>>(people));

    }
}
