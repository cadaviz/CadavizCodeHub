using AutoFixture;
using CadavizCodeHub.Framework.Tests.Fixtures;

namespace CadavizCodeHub.Framework.Tests.Tools
{
    public abstract class TestsBase
    {
        protected Fixture Fixture { get; }

        protected TestsBase()
        {
            Fixture = FixtureHelper.CreateFixture();
        }
    }
}
