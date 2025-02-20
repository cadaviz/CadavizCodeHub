using CadavizCodeHub.Application.EventHandlers;
using CadavizCodeHub.Domain.DomainEvents;
using CadavizCodeHub.TestFramework.Tools;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CadavizCodeHub.Application.UnitTests.EventHandlers
{
    public class OrderCreatedEventHandlerTests : TestsBase
    {
        [Fact]
        public void OrderCreatedEventHandler_ShouldCompleteTask_WhenEventIsHandled()
        {
            // Arrange
            var notification = new OrderCreatedEvent(Guid.NewGuid(), DateTime.Now);

            // Act
            var result = new OrderCreatedEventHandler().Handle(notification, CancellationToken.None);

            // Assert
            result.Should().Be(Task.CompletedTask);
        }
    }
}