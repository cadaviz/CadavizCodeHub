﻿using CadavizCodeHub.Core.WebApi.Responses;
using CadavizCodeHub.Tests.Shared.Shared;
using FluentAssertions;
using System;
using Xunit;

namespace CadavizCodeHub.Core.Tests.Presentation.WebApi.Responses
{
    public class ApplicationErrorResponseTests : TestBase
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
            Func<ApplicationErrorResponse> act = () => new ApplicationErrorResponse(500, message);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
