using CadavizCodeHub.Framework.Extensions;
using CadavizCodeHub.Framework.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace CadavizCodeHub.WebApi.Exceptions
{
    internal class HttpResponseExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpResponseExceptionFilter> _logger;

        public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                return;
            }

            context.Result = CreateErrorResponse(context.Exception);
            context.ExceptionHandled = true;

            _logger.LogError(context.Exception,
                "Error occurred while processing request at {RequestPath}, QueryString: {QueryString}. Result: {Result}",
                context.HttpContext.Request.Path,
                context.HttpContext.Request.QueryString,
                context.Result.SerializeForLog());
        }

        private static ObjectResult CreateErrorResponse(Exception? exception)
        {
            var defaultErrorMessage = "Something went wrong!";
            var devErrorMessage = exception?.Message ?? null;
            var applicationMessages = new ApplicationMessage[] { new(defaultErrorMessage, devErrorMessage) };

            var response = new ApplicationErrorResponse(statusCode: StatusCodes.Status500InternalServerError,
                                                        messages: applicationMessages);

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
