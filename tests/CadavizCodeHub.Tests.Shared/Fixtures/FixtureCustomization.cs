using AutoFixture;
using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CadavizCodeHub.Tests.Shared.Fixtures
{
    public class FixtureCustomization<T>
    {
        public IFixture Fixture { get; }

        public FixtureCustomization(IFixture fixture)
        {
            Fixture = fixture;
        }

        public FixtureCustomization<T> With<TProp>(Expression<Func<T, TProp>> expr, TProp value)
        {
            Fixture.Customizations.Add(new OverridePropertyBuilder<T, TProp>(expr, value));

            return this;
        }

        public T Create() => Fixture.Create<T>();

        public IEnumerable<T> CreateMany(int count) => Fixture.CreateMany<T>(count);

        public void AddConcreteType<TBase, TConcrete>() => Fixture.Customizations.Add(new TypeRelay(typeof(TBase), typeof(TConcrete)));
    }

    public static class CompositionExt
    {
        public static FixtureCustomization<T> For<T>(this IFixture fixture) => new(fixture);

        public static FixtureCustomization<T> ForRecursive<T>(this IFixture fixture)
        {
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return new(fixture);
        }
    }
}
