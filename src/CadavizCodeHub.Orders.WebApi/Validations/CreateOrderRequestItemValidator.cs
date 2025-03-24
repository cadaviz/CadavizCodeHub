using CadavizCodeHub.Core.Shared.Validators;
using CadavizCodeHub.Orders.WebApi.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CadavizCodeHub.Orders.WebApi.Validations
{
    internal class CreateOrderRequestItemValidator : ValidatorBase<CreateOrderRequestItem>
    {
        public CreateOrderRequestItemValidator() : base()
        {
            RuleFor(x => x.Product)
                .NotNull()
                .WithMessage("Product must not be null.")
                .SetValidator(new CreateOrderRequestProductValidator());

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString());
        }
    }
}
