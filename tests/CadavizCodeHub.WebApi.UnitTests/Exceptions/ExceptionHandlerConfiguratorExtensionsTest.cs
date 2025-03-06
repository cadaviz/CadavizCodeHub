using CadavizCodeHub.Framework.Responses;
using CadavizCodeHub.TestFramework.Tools;
using CadavizCodeHub.WebApi.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using Xunit;

namespace CadavizCodeHub.WebApi.UnitTests.Exceptions
{
    public class ExceptionHandlerConfiguratorExtensionsTest : TestsBase
    {
        [Fact]
        public void OnException_ShouldSet500ErrorResponse_WhenUnhandledExceptionOccurs()
        {
            // Arrange
            var filter = new HttpResponseExceptionFilter();
            var exception = new Exception("Unexpected error");

            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var exceptionContext = new ExceptionContext(actionContext, [])
            {
                Exception = exception
            };

            // Act
            filter.OnException(exceptionContext);

            // Assert
            exceptionContext.ExceptionHandled.Should().BeTrue();
            exceptionContext.Result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

            var errorResponse = (exceptionContext.Result as ObjectResult)?.Value as ApplicationErrorResponse;
            errorResponse.Should().NotBeNull();
            errorResponse!.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

            var applicationMessage = errorResponse.Messages.Should().ContainSingle().Which;
            applicationMessage.Message.Should().Be("Something went wrong!");
            applicationMessage.DeveloperMessage.Should().Be("Unexpected error");
        }

        [Fact]
        public void OnException_ShouldSet500ErrorResponse_WhenHandledExceptionOccurs()
        {
            // Arrange
            var filter = new HttpResponseExceptionFilter();
            var exception = new Exception("Unexpected error");

            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var exceptionContext = new ExceptionContext(actionContext, [])
            {
                Exception = exception,
                ExceptionHandled = true,
            };

            // Act
            filter.OnException(exceptionContext);

            // Assert
            exceptionContext.ExceptionHandled.Should().BeTrue();
            exceptionContext.Result.Should().BeNull();
        }
    }
}
