using AutoFixture;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Framework.Tests.Fixtures;
using System.Collections.Generic;

namespace CadavizCodeHub.Unit.Builders
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
