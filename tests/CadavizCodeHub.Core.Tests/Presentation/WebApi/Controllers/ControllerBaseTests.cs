using CadavizCodeHub.Core.Shared.Validators;
using CadavizCodeHub.Core.WebApi.Requests;
using CadavizCodeHub.Core.WebApi.Responses;
using CadavizCodeHub.Tests.Shared.Tools;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using Xunit;
using CoreControllers = CadavizCodeHub.Core.WebApi.Controllers;

namespace CadavizCodeHub.Core.Tests.Presentation.WebApi.Controllers
{
    public class ControllerBaseTests : TestsBase
    {
        private readonly ControllerBaseTest _controller;

        public ControllerBaseTests()
        {
            var loggerMock = new Mock<ILogger<ControllerBaseTest>>();
            _controller = new ControllerBaseTest(loggerMock.Object);
        }

        [Fact]
        public void BadRequest_ShouldReturnBadRequestObjectResult_WhenValidationFails()
        {
            // Arrange
            var validationResult = new ValidationResult([new ValidationFailure("PropertyName", "ErrorMessage")]);
            var expectedMessages = validationResult.Errors.Select(x => new ApplicationMessage(x.ErrorMessage));

            // Act
            var result = _controller.BadRequest(validationResult);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Which;
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestResult.Value.Should().NotBeNull();

            var applicationErrorResponse = badRequestResult.Value.Should().BeOfType<ApplicationErrorResponse>().Which;
            applicationErrorResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            applicationErrorResponse.Messages.Should().NotBeNullOrEmpty();
            applicationErrorResponse.Messages.Should().BeEquivalentTo(expectedMessages);
        }

        [Fact]
        public void OkOrNoContent_ShouldReturnOkObjectResult_WhenResponseIsNotNull()
        {
            // Arrange
            var response = new ResponseTest();

            // Act
            var result = _controller.OkOrNoContent(response);

            // Assert
            result.Should().NotBeNull();
            var okObjectResult = result.Should().BeOfType<OkObjectResult>().Which;
            okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void OkOrNoContent_ShouldReturnNoContentResult_WhenResponseIsNull()
        {
            // Arrange
            IResponse? response = null;

            // Act
            var result = _controller.OkOrNoContent(response);

            // Assert
            result.Should().NotBeNull();
            var noContentResult = result.Should().BeOfType<NoContentResult>().Which;
            noContentResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public void OkOrNotFound_ShouldReturnOkObjectResult_WhenResponseIsNotNull()
        {
            // Arrange
            var response = new ResponseTest();

            // Act
            var result = _controller.OkOrNotFound(response);

            // Assert
            result.Should().NotBeNull();
            var okObjectResult = result.Should().BeOfType<OkObjectResult>().Which;
            okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void OkOrNotFound_ShouldReturnNotFoundResult_WhenResponseIsNull()
        {
            // Arrange
            IResponse? response = null;
            var expectedMessage = new ApplicationMessage("No resource found.");

            // Act
            var result = _controller.OkOrNotFound(response);

            // Assert
            result.Should().NotBeNull();
            var notFoundObjectResult = result.Should().BeOfType<NotFoundObjectResult>().Which;
            notFoundObjectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            notFoundObjectResult.Value.Should().NotBeNull();

            var applicationErrorResponse = notFoundObjectResult.Value.Should().BeOfType<ApplicationErrorResponse>().Which;
            applicationErrorResponse.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            applicationErrorResponse.Messages.Should().NotBeNullOrEmpty();
            applicationErrorResponse.Messages.Should().HaveCount(1);
            applicationErrorResponse.Messages.Should().Contain(expectedMessage);
        }

        [Fact]
        public void ValidateRequest_ShouldReturnBadRequest_WhenValidationFails()
        {
            // Arrange
            var request = new RequestTest();

            // Act
            var result = _controller.ValidateRequest<AbstractValidatorFailTest, RequestTest>(request);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Which;
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestResult.Value.Should().NotBeNull();

            var applicationErrorResponse = badRequestResult.Value.Should().BeOfType<ApplicationErrorResponse>().Which;
            applicationErrorResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

            var applicationMessage = applicationErrorResponse.Messages.Should().NotBeNullOrEmpty().And.ContainSingle().Which;
            applicationMessage.Message.Should().Be(AbstractValidatorFailTest.ErrorMessage);
            applicationMessage.DeveloperMessage.Should().BeNull();
        }

        [Fact]
        public void ValidateRequest_ShouldReturnNull_WhenValidationSucceeds()
        {
            // Arrange
            var request = new RequestTest();

            // Act
            var result = _controller.ValidateRequest<AbstractValidatorSuccessTest, RequestTest>(request);

            // Assert
            result.Should().BeNull();
        }
    }

    public class ControllerBaseTest : CoreControllers.ControllerBase
    {
        internal ControllerBaseTest(ILogger<ControllerBaseTest> logger) : base(logger) { }

        internal new IActionResult BadRequest(ValidationResult validationResult) => base.BadRequest(validationResult);

        internal new IActionResult OkOrNoContent(IResponse? response) => base.OkOrNoContent(response);

        internal new IActionResult OkOrNotFound(IResponse? response) => base.OkOrNotFound(response);

        internal new IActionResult? ValidateRequest<TValidator, TRequest>(TRequest request)
            where TValidator : ValidatorBase<TRequest>, new()
            where TRequest : IRequest
            => base.ValidateRequest<TValidator, TRequest>(request);
    }

    internal class AbstractValidatorSuccessTest : ValidatorBase<RequestTest>
    {
        public AbstractValidatorSuccessTest() : base() { }
    }

    internal class AbstractValidatorFailTest : ValidatorBase<RequestTest>
    {
        internal static string ErrorMessage = "The request is invalid";

        public AbstractValidatorFailTest() : base()
        {
            RuleFor(x => x).Custom((_, context) => context.AddFailure(StatusCodes.Status400BadRequest.ToString(), ErrorMessage));
        }
    }

    internal class ResponseTest : IResponse { }

    internal class RequestTest : IRequest { }
}
