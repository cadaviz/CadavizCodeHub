using AutoFixture;
using CadavizCodeHub.Orders.WebApi.Requests;
using CadavizCodeHub.Orders.WebApi.Validations;
using CadavizCodeHub.Tests.Shared.Tools;
using FluentValidation.TestHelper;
using Xunit;

namespace CadavizCodeHub.Orders.WebApi.Tests.Validations
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
            var requestItem = Fixture.Create<CreateOrderRequestItem>() with { Product = null! };

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
