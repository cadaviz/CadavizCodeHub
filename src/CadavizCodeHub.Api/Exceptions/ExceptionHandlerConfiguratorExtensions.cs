using CadavizCodeHub.Framework.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Api.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class HttpResponseExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                return;
            }

            context.Result = CreateErrorResponse(context.Exception);
            context.ExceptionHandled = true;
        }

        private static ObjectResult CreateErrorResponse(Exception? exception)
        {
            var defaultErrorMessage = "Something went wrong!";
            var devErrorMessage = exception?.Message ?? string.Empty;
            var applicationMessages = new ApplicationMessage[] { new ApplicationMessage(defaultErrorMessage, devErrorMessage) };

            var response = new ApplicationErrorResponse(StatusCode: StatusCodes.Status500InternalServerError,
                                                        Messages: applicationMessages);

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
