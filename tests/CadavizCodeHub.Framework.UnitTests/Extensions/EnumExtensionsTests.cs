using CadavizCodeHub.Framework.Extensions;
using CadavizCodeHub.TestFramework.Tools;
using FluentAssertions;
using System;
using Xunit;

namespace CadavizCodeHub.Framework.UnitTests.Extensions
{
    public class EnumExtensionsTests : TestsBase
    {
        private enum SourceEnum { One, Two, Three }
        private enum DestinationEnum { One, Two, Three, Four }

        [Fact]
        public void Convert_ShouldConvertEnum_WhenValueExistsInDestinationEnum()
        {
            // Act
            var result = EnumExtensions.Convert<SourceEnum, DestinationEnum>(SourceEnum.Two);

            // Assert
            result.Should().Be(DestinationEnum.Two);
        }

        [Fact]
        public void Convert_ShouldReturnDefaultValue_WhenValueDoesNotExistInDestinationEnum()
        {
            // Arrange
            var defaultValue = DestinationEnum.Four;

            // Act
            var result = EnumExtensions.Convert<SourceEnum, DestinationEnum>((SourceEnum)999, defaultValue);

            // Assert
            result.Should().Be(defaultValue);
        }

        [Fact]
        public void Convert_ShouldThrowArgumentException_WhenValueDoesNotExistAndNoDefaultProvided()
        {
            // Act
            var action = () => EnumExtensions.Convert((SourceEnum)999, (DestinationEnum?)null);

            // Assert
            action.Should().Throw<ArgumentException>()
                .WithMessage($"Error while converting an {typeof(SourceEnum)} enum into an {typeof(DestinationEnum)}.");
        }
    }
}