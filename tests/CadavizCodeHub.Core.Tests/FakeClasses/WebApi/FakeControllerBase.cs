using CadavizCodeHub.Core.Shared.Validators;
using CadavizCodeHub.Core.WebApi.Requests;
using CadavizCodeHub.Core.WebApi.Responses;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreControllers = CadavizCodeHub.Core.WebApi.Controllers;

namespace CadavizCodeHub.Core.Tests.FakeClasses.WebApi
{
    public class FakeControllerBase : CoreControllers.ControllerBase
    {
        internal FakeControllerBase(ILogger<FakeControllerBase> logger) : base(logger) { }

        internal new IActionResult BadRequest(ValidationResult validationResult) => base.BadRequest(validationResult);

        internal new IActionResult OkOrNoContent(IResponse? response) => base.OkOrNoContent(response);

        internal new IActionResult OkOrNotFound(IResponse? response) => base.OkOrNotFound(response);

        internal new IActionResult? ValidateRequest<TValidator, TRequest>(TRequest request)
            where TValidator : ValidatorBase<TRequest>, new()
            where TRequest : IRequest
            => base.ValidateRequest<TValidator, TRequest>(request);
    }
}
