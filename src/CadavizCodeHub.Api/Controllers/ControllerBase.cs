using CadavizCodeHub.Api.Requests;
using CadavizCodeHub.Api.Responses;
using CadavizCodeHub.Framework.Responses;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Mvc = Microsoft.AspNetCore.Mvc;

namespace CadavizCodeHub.Api.Controllers
{
    public abstract class ControllerBase : Mvc.ControllerBase
    {
        protected ControllerBase() : base() { }

        protected IActionResult BadRequest(ValidationResult validationResult)
        {
            var response = new ApplicationErrorResponse(StatusCode: StatusCodes.Status400BadRequest,
                                                        Messages: validationResult.Errors.Select(x => new ApplicationMessage(x.ErrorMessage)));
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

        protected IActionResult? ValidateRequest<TValidator, TRequest>(TRequest request)
            where TValidator : AbstractValidator<TRequest>, new()
            where TRequest : IRequest
        {
            var validationResult = new TValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }

            return null;
        }

        protected Uri BuildLocationUri(string pathValue)
        {
            var location = new UriBuilder(scheme: Request.Scheme,
                                          host: Request.Host.Host,
                                          port: Request.Host.Port.GetValueOrDefault(),
                                          pathValue: pathValue);

            return location.Uri;
        }
    }
}
