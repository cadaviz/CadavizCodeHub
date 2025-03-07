using AutoFixture;
using AutoMapper;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.TestFramework.Tools;
using CadavizCodeHub.WebApi.Mappers;
using CadavizCodeHub.WebApi.Requests;
using FluentAssertions;
using Xunit;

namespace CadavizCodeHub.WebApi.UnitTests.Mappers
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