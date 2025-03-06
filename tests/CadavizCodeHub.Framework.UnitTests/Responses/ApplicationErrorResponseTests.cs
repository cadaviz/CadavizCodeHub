using CadavizCodeHub.Framework.Responses;
using CadavizCodeHub.TestFramework.Tools;
using FluentAssertions;
using System;
using Xunit;

namespace CadavizCodeHub.Framework.UnitTests.Responses
{
    public class ApplicationErrorResponseTests : TestsBase
    {
        [Fact]
        public void Constructor_ShouldSetStatusCodeAndMessages()
        {
            // Arrange
            var message = new ApplicationMessage("Error", "Details");

            // Act
            var response = new ApplicationErrorResponse(400, message);

            // Assert
            response.StatusCode.Should().Be(400);
            response.Messages.Should().ContainSingle()
                    .Which.Should().Be(message);
        }

        [Fact]
        public void Constructor_ShouldAcceptMultipleMessages()
        {
            // Arrange
            var messages = new[]
            {
                new ApplicationMessage("Error 1"),
                new ApplicationMessage("Error 2", "Details 2")
            };

            // Act
            var response = new ApplicationErrorResponse(500, messages);

            // Assert
            response.Messages.Should().HaveCount(2);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenMessageIsNullOrEmpty()
        {
            // Arrange
            ApplicationMessage message = null!;

            // Act
            Action act = () => new ApplicationErrorResponse(500, message);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
