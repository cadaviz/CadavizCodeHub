using CadavizCodeHub.Framework.Responses;
using CadavizCodeHub.Framework.Validators;
using CadavizCodeHub.WebApi.Requests;
using CadavizCodeHub.WebApi.Responses;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Mvc = Microsoft.AspNetCore.Mvc;

namespace CadavizCodeHub.WebApi.Controllers
{
    public abstract class ControllerBase : Mvc.ControllerBase
    {
        protected ControllerBase() : base() { }

        protected IActionResult BadRequest(ValidationResult validationResult)
        {
            var response = new ApplicationErrorResponse(statusCode: StatusCodes.Status400BadRequest,
                                                        messages: validationResult.Errors.Select(x => new ApplicationMessage(x.ErrorMessage)).ToArray());
            return BadRequest(response);
        }

        protected IActionResult OkOrNoContent(IResponse? response)
        {
            if (response is null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        protected IActionResult OkOrNotFound(IResponse? response)
        {
            if (response is null)
            {
                var notFoundResponse = new ApplicationErrorResponse(StatusCodes.Status404NotFound, new ApplicationMessage("No resource found."));

                return NotFound(notFoundResponse);
            }

            return Ok(response);
        }

        protected IActionResult? ValidateRequest<TValidator, TRequest>(TRequest request)
            where TValidator : ValidatorBase<TRequest>, new()
            where TRequest : IRequest
        {
            var validationResult = new TValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }

            return null;
        }
    }
}
