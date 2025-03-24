using AutoFixture;
using CadavizCodeHub.Orders.WebApi.Requests;
using CadavizCodeHub.Orders.WebApi.Validations;
using CadavizCodeHub.Tests.Shared.Shared;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace CadavizCodeHub.Orders.WebApi.Tests.Validations
{
    public class CreateOrderRequestValidatorTests : TestBase
    {
        private readonly CreateOrderRequestValidator _validator = new();

        [Fact]
        public void Validator_ShouldHaveNoErrors_WhenRequestIsValid()
        {
            // Arrange
            var request = Fixture.Create<CreateOrderRequest>();

            // Act 
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenRequestIsNull()
        {
            // Arrange
            CreateOrderRequest? request = null;

            // Act
            var result = _validator.Validate(request!);

            // Assert
            result.IsValid.Should().BeFalse();

            var error = result.Errors.Should().NotBeNullOrEmpty().And.ContainSingle().Which;
            error.ErrorMessage.Should().Be("The request cannot be null.");
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenItemsAreNull()
        {
            // Arrange
            var request = Fixture.Create<CreateOrderRequest>() with { Items = null! };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Items)
                  .WithErrorMessage("The order must contain at least one item.");
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenItemsAreEmpty()
        {
            // Arrange
            var request = Fixture.Create<CreateOrderRequest>() with { Items = [] };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Items)
                  .WithErrorMessage("The order must contain at least one item.");
        }


        [Fact]
        public void Validator_ShouldHaveErrors_WhenRequestItemIsNull()
        {
            // Arrange
            var request = Fixture.Create<CreateOrderRequest>() with { Items = [null!] };

            // Act 
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                  .Which.ErrorMessage.Should().Be("Order contains an invalid item.");
        }
    }
}
