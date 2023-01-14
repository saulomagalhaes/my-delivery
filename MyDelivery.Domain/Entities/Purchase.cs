using MyDelivery.Domain.Validations;
namespace MyDelivery.Domain.Entities;

public class Purchase
{
    public int Id { get; private set; }
    public int PersonId { get; private set; }
    public int ProductId { get; private set; }
    public DateTime Date { get; private set; }

    public Person Person { get; set; }
    public Product Product { get; set; }

    public Purchase(int productId, int personId )
    {
        Validate(productId, personId);
    }

    public Purchase(int id, int productId, int personId)
    {
        DomainValidationException.If(id <= 0, "O Id deve ser maior que zero");
        Id = id;
        Validate(productId, personId);
    }

    private void Validate(int productId, int personId)
    {
        DomainValidationException.If(productId <= 0, "O Produto é inválido");
        DomainValidationException.If(personId <= 0, "A Pessoa é inválida");

        PersonId = personId;
        ProductId = productId;
        Date = DateTime.Now;
    }
}

