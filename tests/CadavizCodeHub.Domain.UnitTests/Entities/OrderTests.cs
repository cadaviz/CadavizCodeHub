using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Test.Builders.Builders;
using CadavizCodeHub.TestFramework.Tools;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace CadavizCodeHub.Domain.UnitTests.Entities
{
    public class OrderTests : TestsBase
    {
        [Fact]
        public void Order_ShouldHaveImmutableItems_WhenItemsAreModifiedExternally()
        {
            // Arrange
            var itemCount = 5;
            var items = OrderBuilder.BuildItems(itemCount);
            var order = OrderBuilder.Build(items: items);

            // Act
            var otherList = order.Items.ToList();
            otherList.Clear();

            // Assert
            order.Items.Should().HaveCount(itemCount);
        }

        [Fact]
        public void Order_ShouldAlwaysInitializeItems_WhenCreatedWithNull()
        {
            // Arrange & Act
            var order = new Order(items: null);

            // Assert
            order.Items.Should().NotBeNull();
            order.Items.Should().BeEmpty();
        }

        [Fact]
        public void Order_ShouldCalculateTotalCorrectly_WhenItemsArePresent()
        {
            // Arrange
            var itemCount = new Random().Next(0, 10);
            var items = OrderBuilder.BuildItems(itemCount);
            var order = OrderBuilder.Build(items: items);

            // Act
            var sum = order.Items.Sum(item => item.Total);

            // Assert
            order.Total.Should().Be(sum);
        }

        [Fact]
        public void Order_ShouldCalculateTotalAsZero_WhenNoItemsArePresent()
        {
            // Arrange
            var order = new Order(items: null);

            // Act & Assert
            order.Total.Should().Be(0);
        }

        [Fact]
        public void Order_ShouldHaveEmptyItems_WhenCreatedWithDefaultConstructor()
        {
            // Arrange & Act
            var order = (Order)Activator.CreateInstance(typeof(Order), nonPublic: true)!;

            // Assert
            order.Items.Should().NotBeNull();
            order.Items.Should().BeEmpty();
        }
    }
}
