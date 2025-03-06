using CadavizCodeHub.Application.Services;
using CadavizCodeHub.Domain.DomainEvents;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;
using CadavizCodeHub.Test.Builders.Builders;
using CadavizCodeHub.TestFramework.Tools;
using FluentAssertions;
using MediatR;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CadavizCodeHub.Application.UnitTests.Services
{
    public class OrderApplicationServiceTests : TestsBase
    {
        private readonly IMediator _mediator;
        private readonly IOrderCrudRepository _orderRepository;
        private readonly OrderApplicationService _orderService;

        public OrderApplicationServiceTests()
        {
            _mediator = Substitute.For<IMediator>();
            _orderRepository = Substitute.For<IOrderCrudRepository>();
            _orderService = new OrderApplicationService(_mediator, _orderRepository);
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
            await _orderRepository.Received(1).CreateAsync(order, cancellationToken);
            await _mediator.Received(1).Publish(Arg.Is<OrderCreatedEvent>(e => e.OrderId == order.Id), cancellationToken);

            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task GetOrderAsync_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var order = OrderBuilder.Build();
            var orderId = order.Id;

            _orderRepository
                .GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Order?>(order));

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

            _orderRepository
                .GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Order?>(null));

            // Act
            var result = await _orderService.GetOrderAsync(orderId, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
