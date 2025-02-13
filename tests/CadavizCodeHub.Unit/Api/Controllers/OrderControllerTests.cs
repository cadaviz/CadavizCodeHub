using AutoFixture;
using CadavizCodeHub.Api.Controllers;
using CadavizCodeHub.Api.Requests;
using CadavizCodeHub.Api.Responses;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Services;
using CadavizCodeHub.Framework.Responses;
using CadavizCodeHub.Framework.Tests.Extensions;
using CadavizCodeHub.Framework.Tests.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CadavizCodeHub.Unit.Api.Controllers
{
    public class OrderControllerTests : TestsBase
    {
        private readonly IOrderService _orderService;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _orderService = Substitute.For<IOrderService>();
            _controller = BuildController();
        }

        [Fact]
        public async Task CreateOrder_WithValidRequest_Returns201Created()
        {
            // Arrange
            var request = Fixture.Create<CreateOrderRequest>();
            var order = Fixture.Create<Order>();
            _orderService.CreateOrderAsync(Arg.Any<Order>()).Returns(order);

            // Act
            var result = await _controller.CreateOrder(request);

            // Assert
            var createdResult = result as CreatedResult;
            createdResult.Should().NotBeNull();
            createdResult!.StatusCode.Should().Be(StatusCodes.Status201Created);
            createdResult.Value.Should().NotBeNull();
            createdResult.Value.Should().BeOfType<OrderResponse>();
            createdResult.Location.Should().NotBeNullOrEmpty();
            createdResult.Location.Should().BeValidUri();
        }

        [Fact]
        public async Task CreateOrder_WithInvalidRequest_Returns400BadRequest()
        {
            // Arrange
            var request = Fixture.Create<CreateOrderRequest>() with { Items = Enumerable.Empty<CreateOrderRequestItem>() };

            // Act
            var result = await _controller.CreateOrder(request);

            // Assert
            var badRequestObjectResult = result as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();
            badRequestObjectResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

            var applicationErrorResponse = badRequestObjectResult.Value as ApplicationErrorResponse;
            applicationErrorResponse.Should().NotBeNull();
            applicationErrorResponse!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            applicationErrorResponse!.Messages.Should().NotBeNullOrEmpty();
        }

        private OrderController BuildController()
        {
            var httpRequestFeature = new HttpRequestFeature()
            {
                Scheme = "http",
                PathBase = "localhost",
            };

            var features = new FeatureCollection();
            features.Set<IHttpRequestFeature>(httpRequestFeature);
            features.Set<IHttpResponseFeature>(new HttpResponseFeature());

            var controller = new OrderController(_orderService)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext(features),
                }
            };
            controller.Request.Host = new HostString("localhost");

            return controller;
        }
    }
}
