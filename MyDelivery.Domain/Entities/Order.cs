using MyDelivery.Domain.Validations;
namespace MyDelivery.Domain.Entities;

public class Order
{
    public int Id { get; private set; }
    public int PersonId { get; private set; }
    public int ProductId { get; private set; }
    public DateTime Date { get; private set; }

    public Person Person { get; set; }
    public Product Product { get; set; }

    public Order(int productId, int personId, DateTime date)
    {
        Validate(productId, personId, date);
    }

    public Order(int id, int productId, int personId, DateTime date)
    {
        DomainValidationException.If(id < 0, "O Id deve ser maior que zero");
        Id = id;
        Validate(productId, personId, date);
    }

    private void Validate(int productId, int personId, DateTime? date)
    {
        DomainValidationException.If(productId < 0, "O Produto é inválido");
        DomainValidationException.If(personId < 0, "A Pessoa é inválida");
        DomainValidationException.If(!date.HasValue, "A Data é inválida");

        PersonId = personId;
        ProductId = productId;
        Date = date.Value;
    }
}

