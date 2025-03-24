using CadavizCodeHub.Core.Shared.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CadavizCodeHub.Core.Tests.FakeClasses.WebApi
{
    internal class FakeSuccessValidator : ValidatorBase<FakeRequest>
    {
        public FakeSuccessValidator() : base() { }
    }

    internal class FakeFailValidator : ValidatorBase<FakeRequest>
    {
        internal static string ErrorMessage = "The request is invalid";

        public FakeFailValidator() : base()
        {
            RuleFor(x => x).Custom((_, context) => context.AddFailure(StatusCodes.Status400BadRequest.ToString(), ErrorMessage));
        }
    }
}
