using CadavizCodeHub.Core.Tests.FakeClasses.WebApi;
using CadavizCodeHub.Core.WebApi.Responses;
using CadavizCodeHub.Tests.Shared.Shared;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using Xunit;

namespace CadavizCodeHub.Core.Tests.Presentation.WebApi.Controllers
{
    public class ControllerBaseTests : TestBase
    {
        private readonly FakeControllerBase _controller;

        public ControllerBaseTests()
        {
            var loggerMock = new Mock<ILogger<FakeControllerBase>>();
            _controller = new FakeControllerBase(loggerMock.Object);
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
            var response = new FakeResponse();

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
            var response = new FakeResponse();

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
            var request = new FakeRequest();

            // Act
            var result = _controller.ValidateRequest<FakeFailValidator, FakeRequest>(request);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Which;
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestResult.Value.Should().NotBeNull();

            var applicationErrorResponse = badRequestResult.Value.Should().BeOfType<ApplicationErrorResponse>().Which;
            applicationErrorResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

            var applicationMessage = applicationErrorResponse.Messages.Should().NotBeNullOrEmpty().And.ContainSingle().Which;
            applicationMessage.Message.Should().Be(FakeFailValidator.ErrorMessage);
            applicationMessage.DeveloperMessage.Should().BeNull();
        }

        [Fact]
        public void ValidateRequest_ShouldReturnNull_WhenValidationSucceeds()
        {
            // Arrange
            var request = new FakeRequest();

            // Act
            var result = _controller.ValidateRequest<FakeSuccessValidator, FakeRequest>(request);

            // Assert
            result.Should().BeNull();
        }
    }
}
