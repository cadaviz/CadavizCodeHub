using AutoFixture;
using CadavizCodeHub.Orders.Domain.Entities;
using CadavizCodeHub.Tests.Shared.Fixtures;
using System.Collections.Generic;

namespace CadavizCodeHub.Tests.Shared.Builders.Orders
{
    public static class OrderBuilder
    {
        private static readonly Fixture Fixture = FixtureHelper.CreateFixture();

        public static Order Build(IEnumerable<Item>? items = null)
        {
            items ??= BuildItems();

            return new Order(items);
        }

        public static IEnumerable<Item> BuildItems(int? quantity = null)
        {
            int itemsDefaultQuantity = 2;
            return Fixture.CreateMany<Item>(quantity ?? itemsDefaultQuantity);
        }
    }
}
