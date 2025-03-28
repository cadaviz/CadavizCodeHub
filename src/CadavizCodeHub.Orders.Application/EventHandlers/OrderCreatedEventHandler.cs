﻿using CadavizCodeHub.Core.Logging.Extensions;
using CadavizCodeHub.Orders.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CadavizCodeHub.Orders.Application.EventHandlers
{
    internal class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedEventHandler> _logger;

        public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
        {
            _logger = logger;
        }


        public Task Handle(OrderCreatedEvent notification,  CancellationToken cancellationToken)
        {
            _logger.LogDebugIfEnabled("Order created event handler {EventHandlerName} started.", nameof(OrderCreatedEventHandler));

            return Task.CompletedTask;
        }
    }
}
