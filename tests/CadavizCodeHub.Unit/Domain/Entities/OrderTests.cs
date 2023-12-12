using System;
using System.Linq;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Framework.Tests.Tools;
using CadavizCodeHub.Unit.Builders;
using FluentAssertions;
using Xunit;

namespace CadavizCodeHub.Unit.Domain.Entities
{
    public class OrderTests : TestsBase
    {
        [Fact]
        public void Order_ItemsProperty_IsImmutable()
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
        public void Order_ItemsProperty_AlwaysIsInitialized()
        {
            // Arrange & Act
            var order = new Order(items: null);

            // Assert
            order.Items.Should().NotBeNull();
            order.Items.Should().BeEmpty();
        }

        [Fact]
        public void Order_TotalProperty_MustBeTheSumOfItemsTotal()
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
    }
}
