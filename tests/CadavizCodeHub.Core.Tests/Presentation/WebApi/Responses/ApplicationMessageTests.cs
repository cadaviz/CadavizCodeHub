using CadavizCodeHub.Core.WebApi.Responses;
using CadavizCodeHub.Tests.Shared.Tools;
using FluentAssertions;
using System;
using Xunit;

namespace CadavizCodeHub.Framework.UnitTests.Responses
{
    public class ApplicationMessageTests : TestsBase
    {
        [Theory]
        [InlineData("Error", "StackTrace")]
        [InlineData("Error", null)]
        public void Constructor_ShouldSetMessageAndDeveloperMessage(string message, string? developerMessage)
        {
            // Arrange & Act
            var applicationMessage = new ApplicationMessage(message, developerMessage);

            // Assert
            applicationMessage.Message.Should().Be(message);
            applicationMessage.DeveloperMessage.Should().Be(developerMessage);
        }

        [Fact]
        public void Constructor_ShouldSetDeveloperMessageToNull_WhenNotProvided()
        {
            // Act
            var message = new ApplicationMessage("Error");

            // Assert
            message.DeveloperMessage.Should().BeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_ShouldThrowArgumentException_WhenMessageIsNullOrEmpty(string? invalidMessage)
        {
            // Act
            Func<ApplicationMessage> act = () => new ApplicationMessage(invalidMessage!);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
