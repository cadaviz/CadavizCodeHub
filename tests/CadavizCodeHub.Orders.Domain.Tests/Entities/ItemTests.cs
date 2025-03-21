using AutoFixture;
using CadavizCodeHub.Orders.Domain.Entities;
using CadavizCodeHub.Tests.Shared.Shared;
using FluentAssertions;
using Xunit;

namespace CadavizCodeHub.Orders.Domain.Tests.Entities
{
    public class ItemTests : TestBase
    {
        [Fact]
        public void Item_ShouldCalculateTotal_WhenQuantityIsMultipliedByProductPrice()
        {
            // Arrange
            var item = Fixture.Create<Item>();

            // Act
            var total = item.Quantity * item.Product.Price;

            // Assert
            item.Total.Should().Be(total);
        }
    }
}
