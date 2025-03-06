using CadavizCodeHub.Framework.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CadavizCodeHub.WebApi.Exceptions
{
    internal class HttpResponseExceptionFilter : IExceptionFilter
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
            var devErrorMessage = exception?.Message ?? null;
            var applicationMessages = new ApplicationMessage[] { new ApplicationMessage(defaultErrorMessage, devErrorMessage) };

            var response = new ApplicationErrorResponse(statusCode: StatusCodes.Status500InternalServerError,
                                                        messages: applicationMessages);

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
