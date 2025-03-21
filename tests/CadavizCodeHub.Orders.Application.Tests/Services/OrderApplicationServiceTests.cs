using CadavizCodeHub.Orders.Application.Services;
using CadavizCodeHub.Orders.Domain.DomainEvents;
using CadavizCodeHub.Orders.Domain.Entities;
using CadavizCodeHub.Orders.Domain.Repositories;
using CadavizCodeHub.Tests.Shared.Builders.Orders;
using CadavizCodeHub.Tests.Shared.Shared;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CadavizCodeHub.Orders.Application.Tests.Services
{
    public class OrderApplicationServiceTests : TestBase
    {
        private readonly Mock<ILogger<OrderApplicationService>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IOrderCrudRepository> _orderRepositoryMock;
        private readonly OrderApplicationService _orderService;

        public OrderApplicationServiceTests()
        {
            _loggerMock = new Mock<ILogger<OrderApplicationService>>();
            _mediatorMock = new Mock<IMediator>();
            _orderRepositoryMock = new Mock<IOrderCrudRepository>();
            _orderService = new OrderApplicationService(_loggerMock.Object, _mediatorMock.Object, _orderRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldSaveOrderAndPublishEvent()
        {
            // Arrange
            var order = OrderBuilder.Build();
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _orderService.CreateOrderAsync(order, cancellationToken);

            // Assert
            _orderRepositoryMock.Verify(repo => repo.CreateAsync(order, cancellationToken), Times.Once);
            _mediatorMock.Verify(mediator => mediator.Publish(It.Is<OrderCreatedEvent>(e => e.OrderId == order.Id), cancellationToken), Times.Once);

            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task GetOrderAsync_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var order = OrderBuilder.Build();
            var orderId = order.Id;

            _orderRepositoryMock
                .Setup(repo => repo.GetByIdAsync(orderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderAsync(orderId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task GetOrderAsync_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            _orderRepositoryMock
                .Setup(repo => repo.GetByIdAsync(orderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Order?)null);

            // Act
            var result = await _orderService.GetOrderAsync(orderId, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
