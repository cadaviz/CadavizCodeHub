using CadavizCodeHub.Core.Shared.Validators;
using CadavizCodeHub.Orders.WebApi.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CadavizCodeHub.WebApi.Validations
{
    internal class CreateOrderRequestValidator : ValidatorBase<CreateOrderRequest>
    {
        public CreateOrderRequestValidator() : base()
        {
            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("The order must contain at least one item.")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString())
                .When(x => x is not null);

            RuleForEach(x => x.Items)
                .NotNull().WithMessage("Order contains an invalid item.")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString())
                .SetValidator(new CreateOrderRequestItemValidator());
        }
    }
}
