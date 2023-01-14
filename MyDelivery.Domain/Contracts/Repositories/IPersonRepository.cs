using MyDelivery.Domain.Entities;

namespace MyDelivery.Domain.Contracts.Repositories;

public interface IPersonRepository
{
    Task<ICollection<Person>> GetPeople();
    Task<Person> GetById(int id);
    Task<Person> Create(Person person);
    Task Update(Person person);
    Task Delete(Person person);
    Task<int> GetIdByDocument(string document);
}
