using MyDelivery.Application.DTOs.Person;

namespace MyDelivery.Application.Services.Contracts;

public interface IPersonService
{
    Task<ResultService<PersonDTO>> Create(PersonDTO personDTO);
    Task<ResultService<ICollection<ReadPersonDTO>>> GetPeople();
    Task<ResultService<ReadPersonDTO>> GetById(int id);
    Task<ResultService> Update(int id, PersonDTO personDTO);
    Task<ResultService> Delete(int id);
}
