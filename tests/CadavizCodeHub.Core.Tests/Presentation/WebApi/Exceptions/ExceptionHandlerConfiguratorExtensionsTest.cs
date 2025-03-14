using CadavizCodeHub.Core.WebApi.Exceptions;
using CadavizCodeHub.Core.WebApi.Responses;
using CadavizCodeHub.Tests.Shared.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace CadavizCodeHub.Core.Tests.Presentation.WebApi.Exceptions
{
    public class ExceptionHandlerConfiguratorExtensionsTest : TestsBase
    {
        private readonly HttpResponseExceptionFilter httpResponseExceptionFilter;

        public ExceptionHandlerConfiguratorExtensionsTest()
        {
            var loggerMock = new Mock<ILogger<HttpResponseExceptionFilter>>();

            httpResponseExceptionFilter = new HttpResponseExceptionFilter(loggerMock.Object);
        }

        [Fact]
        public void OnException_ShouldSet500ErrorResponse_WhenUnhandledExceptionOccurs()
        {
            // Arrange
            var exception = new Exception("Unexpected error");

            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var exceptionContext = new ExceptionContext(actionContext, [])
            {
                Exception = exception
            };

            // Act
            httpResponseExceptionFilter.OnException(exceptionContext);

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
            var exception = new Exception("Unexpected error");

            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var exceptionContext = new ExceptionContext(actionContext, [])
            {
                Exception = exception,
                ExceptionHandled = true,
            };

            // Act
            httpResponseExceptionFilter.OnException(exceptionContext);

            // Assert
            exceptionContext.ExceptionHandled.Should().BeTrue();
            exceptionContext.Result.Should().BeNull();
        }
    }
}
