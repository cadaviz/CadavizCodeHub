using AutoMapper;
using CadavizCodeHub.Orders.WebApi.Mappers;
using CadavizCodeHub.Orders.WebApi.Responses;
using CadavizCodeHub.Tests.Shared.Builders.Orders;
using CadavizCodeHub.Tests.Shared.Shared;
using FluentAssertions;
using Xunit;

namespace CadavizCodeHub.Orders.WebApi.Tests.Mappers
{
    public class OrderResponseProfileTests : TestBase
    {
        private readonly IMapper _mapper;

        public OrderResponseProfileTests()
        {
            _mapper = CreateMapper<OrderResponseProfile>();
        }

        [Fact]
        public void OrderResponseProfile_ShouldMapOrderToOrderResponse()
        {
            // Arrange
            var order = OrderBuilder.Build();

            // Act
            var orderResponse = _mapper.Map<OrderResponse>(order);

            // Assert
            orderResponse.Should().NotBeNull();

            order.Should().BeEquivalentTo(orderResponse);
        }
    }
}