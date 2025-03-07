using CadavizCodeHub.Domain.DomainEvents;
using MediatR;

namespace CadavizCodeHub.Application.EventHandlers
{
    internal class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
