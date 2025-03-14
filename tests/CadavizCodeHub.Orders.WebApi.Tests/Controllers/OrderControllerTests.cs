using AutoFixture;
using AutoMapper;
using CadavizCodeHub.Core.WebApi.Responses;
using CadavizCodeHub.Orders.Application.Services;
using CadavizCodeHub.Orders.Domain.Entities;
using CadavizCodeHub.Orders.WebApi.Controllers;
using CadavizCodeHub.Orders.WebApi.Mappers;
using CadavizCodeHub.Orders.WebApi.Requests;
using CadavizCodeHub.Orders.WebApi.Responses;
using CadavizCodeHub.Tests.Shared.Builders.Builders;
using CadavizCodeHub.Tests.Shared.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CadavizCodeHub.Orders.WebApi.Tests.Controllers
{
    public class OrderControllerTests : TestsBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderApplicationService _orderService;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            var loggerMock = new Mock<ILogger<OrderController>>();
            _mapper = CreateMappers(typeof(OrderResponseProfile), typeof(CreateOrderRequestProfile));
            _orderService = Substitute.For<IOrderApplicationService>();
            _controller = new OrderController(loggerMock.Object, _mapper, _orderService);
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnBadRequest_WhenRequestIsInvalid()
        {
            // Arrange
            var request = Fixture.Create<CreateOrderRequest>() with { Items = [] };

            // Act
            var result = await _controller.CreateOrder(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestObjectResult = (BadRequestObjectResult)result;

            badRequestObjectResult.Should().NotBeNull();
            badRequestObjectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestObjectResult.Value.Should().NotBeNull();
            badRequestObjectResult.Value.Should().BeOfType<ApplicationErrorResponse>();

            var applicationErrorResponse = (ApplicationErrorResponse)badRequestObjectResult.Value!;
            applicationErrorResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            applicationErrorResponse.Messages.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnCreated_WhenRequestIsValid()
        {
            // Arrange
            var request = Fixture.Create<CreateOrderRequest>();
            var order = OrderBuilder.Build();
            _orderService.CreateOrderAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>()).Returns(order);

            // Act
            var result = await _controller.CreateOrder(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>();

            var createdAtActionResult = (CreatedAtActionResult)result;
            createdAtActionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            createdAtActionResult.Value.Should().NotBeNull();
            createdAtActionResult.Value.Should().BeOfType<OrderResponse>();
            createdAtActionResult.RouteValues.Should().NotBeNullOrEmpty();
            createdAtActionResult.RouteValues.Should().ContainKey("id");

            var idValue = createdAtActionResult.RouteValues!.GetValueOrDefault("id")!;
            idValue.Should().NotBeNull();
            idValue.Should().NotBe(Guid.Empty);
            idValue.Should().BeOfType<Guid>();
        }

        [Fact]
        public async Task GetOrder_ShouldReturnOk_WhenOrderIsFound()
        {
            // Arrange
            var expectedOrder = OrderBuilder.Build();
            var orderId = expectedOrder.Id;

            _orderService.GetOrderAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(expectedOrder);

            // Act
            var result = await _controller.GetOrderById(orderId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;

            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().NotBeNull();
            okResult.Value.Should().BeOfType<OrderResponse>();
        }

        // Teste para quando o pedido não é encontrado
        [Fact]
        public async Task GetOrder_ShouldReturnNotFound_WhenOrderIsNotFound()
        {
            // Arrange
            Order? expectedOrder = null;
            var orderId = Guid.NewGuid();

            _orderService.GetOrderAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(expectedOrder);

            // Act
            var result = await _controller.GetOrderById(orderId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Which;

            notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            notFoundResult.Value.Should().NotBeNull();

            var applicationErrorResponse = notFoundResult.Value.Should().BeOfType<ApplicationErrorResponse>().Which;
            applicationErrorResponse.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            applicationErrorResponse.Messages.Should().NotBeNullOrEmpty();
        }
    }
}
