using MediatR;
using System;

namespace CadavizCodeHub.Domain.DomainEvents
{
    public class OrderCreatedEvent(Guid orderId, DateTime createdAt) : INotification
    {
        public Guid OrderId { get; init; } = orderId;
        public DateTime CreatedAt { get; init; } = createdAt;
    }
}
