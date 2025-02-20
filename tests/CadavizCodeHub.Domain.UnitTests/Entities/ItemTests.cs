using AutoFixture;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.TestFramework.Tools;
using FluentAssertions;
using Xunit;

namespace CadavizCodeHub.Domain.UnitTests.Entities
{
    public class ItemTests : TestsBase
    {
        [Fact]
        public void Item_TotalProperty_MustBeQuantityTimesProductPrice()
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
