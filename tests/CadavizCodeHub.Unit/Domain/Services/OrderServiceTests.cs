using AutoFixture;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;
using CadavizCodeHub.Domain.Services;
using CadavizCodeHub.Framework.Tests.Tools;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CadavizCodeHub.Unit.Domain.Services
{
    public class OrderServiceTests : TestsBase
    {
        private readonly IOrderCrudRepository _orderRepository;
        private readonly IOrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepository = Substitute.For<IOrderCrudRepository>();
            _orderService = new OrderService( _orderRepository);
        }

        [Fact]
        public async Task CreateOrderAsync_WithValidDomain_ReturnsOrder()
        {
            // Arrange
            var order = Fixture.Create<Order>();
            _orderRepository.CreateAsync(order).Returns(order);

            // Act
            await _orderService.CreateOrderAsync(order);

            // Assert
            order.Should().NotBeNull();
        }

        [Fact]
        public async Task GetOrderAsync_WithAnExistingOrder_ReturnsOrder()
        {
            // Arrange
            var order = Fixture.Create<Order>();
            _orderRepository.GetByIdAsync(order.Id).Returns(order);

            // Act
           var result = await _orderService.GetOrderAsync(order.Id);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(order.Id);
        }

        [Fact]
        public async Task GetOrderAsync_WithoutAnExistingOrder_ReturnsNull()
        {
            // Arrange
            _orderRepository.GetByIdAsync(Arg.Any<Guid>()).Returns((Order?)null);

            // Act
            var result = await _orderService.GetOrderAsync(Guid.NewGuid());

            // Assert
            result.Should().BeNull();
        }
    }
}
