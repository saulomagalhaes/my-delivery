using FluentValidation;
using MyDelivery.Application.DTOs.Product;

namespace MyDelivery.Application.DTOs.Validations;

public class ProductDTOValidator : AbstractValidator<ProductDTO>
{
    public ProductDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("O nome deve ser informado");

        RuleFor(x => x.Code)
            .NotEmpty()
            .NotNull()
            .WithMessage("O código deve ser informado");

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("O preço deve ser maior que 0");
    }
}
