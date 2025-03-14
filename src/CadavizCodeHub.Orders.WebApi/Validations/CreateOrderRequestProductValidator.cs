using CadavizCodeHub.Core.Shared.Validators;
using CadavizCodeHub.Orders.WebApi.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CadavizCodeHub.WebApi.Validations
{
    internal class CreateOrderRequestProductValidator : ValidatorBase<CreateOrderRequestProduct>
    {
        public CreateOrderRequestProductValidator() : base()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Product description cannot be empty.")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString());

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Product price must be greater than zero.")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString());
        }
    }
}
