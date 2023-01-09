using MyDelivery.Domain.Validations;
namespace MyDelivery.Domain.Entities;

public class Person
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Document { get; private set; }
    public string Phone { get; private set; }

    public ICollection<Purchase> Purchases { get; set; }

    public Person(string name, string document, string phone)
    {
        Validate(name, document, phone);
    }

    public Person (int id, string name, string document, string phone)
    {
        DomainValidationException.If(id < 0, "O Id deve ser maior que zero");
        Id = id;
        Validate(name, document, phone);
    }

    private void Validate(string name, string document, string phone)
    {
        DomainValidationException.If(String.IsNullOrWhiteSpace(name), "O Nome é inválido");
        DomainValidationException.If(String.IsNullOrWhiteSpace(document), "O Documento é inválido");
        DomainValidationException.If(String.IsNullOrWhiteSpace(phone), "O Telefone é inválido");

        Name= name;
        Document= document;
        Phone= phone;
    }
}
