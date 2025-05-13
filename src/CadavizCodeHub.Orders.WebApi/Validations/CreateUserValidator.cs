
using CadavizCodeHub.Core.Shared.Validators;
using CadavizCodeHub.Orders.WebApi.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CadavizCodeHub.Orders.WebApi.Validations
{
    public class CreateUserValidator : ValidatorBase<UserRequest>
    {
        public CreateUserValidator() : base()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name could not be empty")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString())
                .When(x => x is not null);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email could not be empty")
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString())
                .When(x => x is not null);
        }
    }
}