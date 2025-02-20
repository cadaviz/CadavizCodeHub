using AutoMapper;
using CadavizCodeHub.Test.Builders.Builders;
using CadavizCodeHub.TestFramework.Tools;
using CadavizCodeHub.WebApi.Mappers;
using CadavizCodeHub.WebApi.Responses;
using FluentAssertions;
using Xunit;

namespace CadavizCodeHub.WebApi.UnitTests.Mappers
{
    public class OrderResponseProfileTests : TestsBase
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