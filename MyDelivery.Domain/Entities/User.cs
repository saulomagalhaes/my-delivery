using MyDelivery.Domain.Validations;

namespace MyDelivery.Domain.Entities;

public class User
{
	public int Id { get; private set; }
	public string Email { get; private set; }
	public string Password { get; private set; }
    public User(string email, string password)
    {
        Validate(email, password);
    }
    public User(int id, string email, string password)
    {
        DomainValidationException.If(id <= 0, "O Id deve ser maior que zero");
        Id = id;
        Validate(email, password);
    }

    private void Validate(string email, string password)
    {
        DomainValidationException.If(String.IsNullOrWhiteSpace(email), "O Email deve ser informado");
        DomainValidationException.If(String.IsNullOrWhiteSpace(password), "A Senha deve ser informada");

        Email = email;
        Password = password;
    }
}
