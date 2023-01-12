using AutoMapper;
using MyDelivery.Application.DTOs.Person;
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
        return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(data), data.Id);
    }

    public async Task<ResultService> Delete(int id)
    {
        var person = await _personRepository.GetById(id);
        if (person == null)
            return ResultService.Fail<ReadPersonDTO>("Pessoa não encontrada");
        await _personRepository.Delete(person);
        return ResultService.Ok("Pessoa deletada");
    }

    public async Task<ResultService<ReadPersonDTO>> GetById(int id)
    {
        var person = await _personRepository.GetById(id);
        if(person == null)
            return ResultService.Fail<ReadPersonDTO>("Pessoa não encontrado");
        return ResultService.Ok<ReadPersonDTO>(_mapper.Map<ReadPersonDTO>(person));
    }

    public async Task<ResultService<ICollection<ReadPersonDTO>>> GetPeople()
    {
        var people = await _personRepository.GetPeople();
        return ResultService.Ok<ICollection<ReadPersonDTO>>(_mapper.Map<ICollection<ReadPersonDTO>>(people));

    }

    public async Task<ResultService> Update(int id, PersonDTO personDTO)
    {
        if(personDTO == null)
            return ResultService.Fail("Informe o objeto corretamente");
        var validation = new PersonDTOValidator().Validate(personDTO);
        if (!validation.IsValid)
            return ResultService.RequestError("Erro na validação do objeto", validation);

        var person = await _personRepository.GetById(id);
        if (person == null)
            return ResultService.Fail("Pessoa não encontrado");

        person = _mapper.Map(personDTO, person);
        await _personRepository.Update(person);
        return ResultService.Ok("Pessoa editada");
    }
}
