using MediatR;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Orders.Domain.DomainEvents
{
    [ExcludeFromCodeCoverage]
    public class OrderCreatedEvent(Guid orderId, DateTime createdAt) : INotification
    {
        public Guid OrderId { get; init; } = orderId;
        public DateTime CreatedAt { get; init; } = createdAt;
    }
}
