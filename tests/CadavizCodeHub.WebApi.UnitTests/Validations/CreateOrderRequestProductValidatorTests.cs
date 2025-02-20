using AutoFixture;
using CadavizCodeHub.TestFramework.Tools;
using CadavizCodeHub.WebApi.Requests;
using CadavizCodeHub.WebApi.Validations;
using FluentValidation.TestHelper;
using Xunit;

namespace CadavizCodeHub.WebApi.UnitTests.Validations
{
   public class CreateOrderRequestProductValidatorTests : TestsBase
    {
        private readonly CreateOrderRequestProductValidator _validator = new();

        [Fact]
        public void Validator_ShouldHaveNoErrors_WhenRequestProductIsValid()
        {
            // Arrange
            var requestProduct = Fixture.Create<CreateOrderRequestProduct>();

            // Act 
            var result = _validator.TestValidate(requestProduct);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validator_ShouldHaveError_WhenRequestProductDescriptionIsEmpty(string description)
        {
            // Arrange
            var requestProduct = Fixture.Build<CreateOrderRequestProduct>()
                                        .With(x => x.Description, description)
                                        .Create();

            // Act
            var result = _validator.TestValidate(requestProduct);

            // Assert
            result.ShouldHaveValidationErrorFor("Description")
                  .WithErrorMessage("Product description cannot be empty.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.99)]
        public void Validator_ShouldHaveError_WhenRequestProductPriceIsLessThanZero(decimal price)
        {
            // Arrange
            var requestProduct = Fixture.Build<CreateOrderRequestProduct>()
                                     .With(x => x.Price, price)
                                     .Create();

            // Act
            var result = _validator.TestValidate(requestProduct);

            // Assert
            result.ShouldHaveValidationErrorFor("Price")
                  .WithErrorMessage("Product price must be greater than zero.");
        }
    }
}
