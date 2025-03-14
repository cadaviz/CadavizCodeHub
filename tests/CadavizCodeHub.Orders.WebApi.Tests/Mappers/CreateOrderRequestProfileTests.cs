using AutoFixture;
using AutoMapper;
using CadavizCodeHub.Orders.Domain.Entities;
using CadavizCodeHub.Orders.WebApi.Mappers;
using CadavizCodeHub.Orders.WebApi.Requests;
using CadavizCodeHub.Tests.Shared.Tools;
using FluentAssertions;
using Xunit;

namespace CadavizCodeHub.Orders.WebApi.Tests.Mappers
{
    public class CreateOrderRequestProfileTests : TestsBase
    {
        private readonly IMapper _mapper;

        public CreateOrderRequestProfileTests()
        {
            _mapper = CreateMapper<CreateOrderRequestProfile>();
        }

        [Fact]
        public void CreateOrderRequestProfile_ShouldMapCreateOrderRequestToOrder()
        {
            // Arrange
            var createOrderRequest = Fixture.Create<CreateOrderRequest>();

            // Act
            var order = _mapper.Map<Order>(createOrderRequest);

            // Assert
            order.Should().NotBeNull();
            order.Should().BeEquivalentTo(createOrderRequest);
        }
    }
}