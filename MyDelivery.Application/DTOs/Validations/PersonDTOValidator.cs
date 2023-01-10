using FluentValidation;

namespace MyDelivery.Application.DTOs.Validations;

public class PersonDTOValidator : AbstractValidator<PersonDTO>
{
    public PersonDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("O nome deve ser informado");

        RuleFor(x => x.Document)
            .NotEmpty()
            .NotNull()
            .WithMessage("O documento deve ser informado");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .NotNull()
            .WithMessage("O telefone deve ser informado");
    }
}
