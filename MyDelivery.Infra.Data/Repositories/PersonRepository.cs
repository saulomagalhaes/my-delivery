using Microsoft.EntityFrameworkCore;
using MyDelivery.Domain.Contracts.Repositories;
using MyDelivery.Domain.Entities;
using MyDelivery.Infra.Data.Context;

namespace MyDelivery.Infra.Data.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly MyDeliveryDbContext _db;
    public PersonRepository(MyDeliveryDbContext db)
    {
        _db = db;
    }
    public async Task<Person> Create(Person person)
    {
        _db.Add(person);
        await _db.SaveChangesAsync();
        return person;
    }

    public async Task Delete(Person person)
    {
        _db.Remove(person);
        await _db.SaveChangesAsync();
    }

    public async Task<Person> GetById(int id)
    {
        return await _db.People.FirstOrDefaultAsync(p => p.Id == id); 
    }

    public async Task<ICollection<Person>> GetPeople()
    {
        return await _db.People.ToListAsync();
    }

    public async Task Update(Person person)
    {
         _db.Update(person);
         await _db.SaveChangesAsync();
    }
}
