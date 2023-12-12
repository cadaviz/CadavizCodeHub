using System;
using AutoFixture;
using CadavizCodeHub.Api.Controllers;
using CadavizCodeHub.Domain.Services;
using CadavizCodeHub.Framework.Tests.Tools;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace CadavizCodeHub.Unit.Controllers
{
    public class OrderControllerTests : TestsBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderCreationService;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _logger = Substitute.For<ILogger<OrderController>>();
            _orderCreationService = Substitute.For<IOrderService>();
            _controller = new OrderController(_logger, _orderCreationService);
        }

        [Fact]
        public void Item_TotalProperty_MustBeQuantityTimesProductPrice()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
