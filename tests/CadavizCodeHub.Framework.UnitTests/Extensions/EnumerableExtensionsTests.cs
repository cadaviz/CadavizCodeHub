using CadavizCodeHub.TestFramework.Tools;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace CadavizCodeHub.Framework.UnitTests.Extensions
{
    public class EnumerableExtensionsTests : TestsBase
    {
        [Theory]
        [InlineData(null, true)]   
        [InlineData(new int[] { }, true)]   
        [InlineData(new int[] { 1, 2, 3 }, false)] 
        [InlineData(new int[] { 1 }, false)]  
        public void IsNullOrEmpty_ShouldReturnExpectedResult(IEnumerable<int>? list, bool expectedResult)
        {
            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            var result = list.IsNullOrEmpty();
#pragma warning restore CS8604 // Possible null reference argument.

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
