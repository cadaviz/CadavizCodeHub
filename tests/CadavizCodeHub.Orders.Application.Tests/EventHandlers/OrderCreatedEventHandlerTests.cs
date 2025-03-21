using CadavizCodeHub.Orders.Application.EventHandlers;
using CadavizCodeHub.Orders.Domain.DomainEvents;
using CadavizCodeHub.Tests.Shared.Tools;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CadavizCodeHub.Orders.Application.Tests.EventHandlers
{
    public class OrderCreatedEventHandlerTests : TestsBase
    {
        [Fact]
        public void OrderCreatedEventHandler_ShouldCompleteTask_WhenEventIsHandled()
        {
            // Arrange
            var notification = new OrderCreatedEvent(Guid.NewGuid(), DateTime.Now);
            var logger = new Mock<ILogger<OrderCreatedEventHandler>>();

            // Act
            var result = new OrderCreatedEventHandler(logger.Object).Handle(notification, CancellationToken.None);

            // Assert
            result.Should().Be(Task.CompletedTask);
        }
    }
}