﻿using AutoFixture;
using CadavizCodeHub.TestFramework.Tools;
using CadavizCodeHub.WebApi.Requests;
using CadavizCodeHub.WebApi.Validations;
using FluentAssertions;
using FluentValidation.TestHelper;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace CadavizCodeHub.WebApi.UnitTests.Validations
{
    public class CreateOrderRequestItemValidatorTests : TestsBase
    {
        private readonly CreateOrderRequestItemValidator _validator = new();

        [Fact]
        public void Validator_ShouldHaveNoErrors_WhenRequestItemIsValid()
        {
            // Arrange
            var requestItem = Fixture.Create<CreateOrderRequestItem>();

            // Act 
            var result = _validator.TestValidate(requestItem);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validator_ShouldHaveErrors_WhenRequestItemProductIsNull()
        {
            // Arrange
            var requestItem = Fixture.Create<CreateOrderRequestItem>() with { Product = null };

            // Act 
            var result = _validator.TestValidate(requestItem);

            // Assert
            result.ShouldHaveValidationErrorFor("Product")
                  .WithErrorMessage("Product must not be null.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validator_ShouldHaveError_WhenRequestItemQuantityIsLessThanZero(int quantity)
        {
            // Arrange
            var requestItem = Fixture.Build<CreateOrderRequestItem>()
                                     .With(x => x.Quantity, quantity)
                                     .Create();

            // Act
            var result = _validator.TestValidate(requestItem);

            // Assert
            result.ShouldHaveValidationErrorFor("Quantity")
                  .WithErrorMessage("Quantity must be greater than zero.");
        }
    }
}
