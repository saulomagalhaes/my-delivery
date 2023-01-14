using FluentValidation;
using MyDelivery.Application.DTOs.Purchase;

namespace MyDelivery.Application.DTOs.Validations;

public class PurchaseDTOValidator : AbstractValidator<PurchaseDTO>
{
	public PurchaseDTOValidator()
	{
		RuleFor(x => x.Code)
            .NotEmpty()
            .NotNull()
            .WithMessage("O codigo deve ser informado");

        RuleFor(x => x.Document)
            .NotEmpty()
            .NotNull()
            .WithMessage("O codigo deve ser informado");
    }
}
