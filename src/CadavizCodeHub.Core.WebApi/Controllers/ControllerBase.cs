using CadavizCodeHub.Core.Logging.Extensions;
using CadavizCodeHub.Core.Shared.Validators;
using CadavizCodeHub.Core.WebApi.Requests;
using CadavizCodeHub.Core.WebApi.Responses;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Mvc = Microsoft.AspNetCore.Mvc;

namespace CadavizCodeHub.Core.WebApi.Controllers
{
    public abstract class ControllerBase : Mvc.ControllerBase
    {
        protected readonly ILogger<ControllerBase> _logger;

        protected ControllerBase(ILogger<ControllerBase> logger) : base()
        {   
            _logger = logger;
        }

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
                _logger.LogDebugIfEnabled("The request in invalid. ValidationErrors='{ValidationErrors}'", request, validationResult);

                return BadRequest(validationResult);
            }

            return null;
        }
    }
}
