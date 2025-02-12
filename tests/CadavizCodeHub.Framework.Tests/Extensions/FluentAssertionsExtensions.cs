using System;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace CadavizCodeHub.Framework.Tests.Extensions
{
    public static class StringAssertionsExtensions
    {
        /// <summary>
        /// Asserts that string is a valid URI
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <paramref name="because" />.
        /// </param>
        public static AndConstraint<StringAssertions<TAssertions>> BeValidUri<TAssertions>(this StringAssertions<TAssertions> parent, string because = "", params object[] becauseArgs)
        where TAssertions : StringAssertions<TAssertions>
        {
            AssertionChain.GetOrCreate()
                .ForCondition(Uri.IsWellFormedUriString(parent.Subject, UriKind.RelativeOrAbsolute))
                .BecauseOf(because, becauseArgs)
                .FailWith($"Expected '{parent.Subject}' to be a valid URI, but it's not.");

            return new AndConstraint<StringAssertions<TAssertions>>(parent);
        }
    }
}
