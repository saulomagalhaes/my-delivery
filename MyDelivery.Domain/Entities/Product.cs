using MyDelivery.Domain.Validations;
namespace MyDelivery.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public decimal Price { get; private set; }

    public ICollection<Purchase> Purchases { get; set; }


    public Product(string name, string code, decimal price)
    {
        Validate(name, code, price);
    }

    public Product(int id, string name, string code, decimal price)
    {
        DomainValidationException.If(id < 0, "O Id deve ser maior que zero");
        Id = id;
        Validate(name, code, price);
    }

    private void Validate(string name, string code, decimal price)
    {
        DomainValidationException.If(String.IsNullOrWhiteSpace(name), "O Nome é inválido");
        DomainValidationException.If(String.IsNullOrWhiteSpace(code), "O Código é inválido");
        DomainValidationException.If(price < 0, "O Preço é inválido");

        Name = name;
        Code = code;
        Price = price;
    }
}
