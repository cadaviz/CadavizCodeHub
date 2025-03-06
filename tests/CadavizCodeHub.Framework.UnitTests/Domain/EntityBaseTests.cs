using CadavizCodeHub.TestFramework.TestClasses.Domain;
using CadavizCodeHub.TestFramework.Tools;
using FluentAssertions;
using System;
using Xunit;

namespace CadavizCodeHub.Framework.UnitTests.Domain
{
    public class EntityBaseTests : TestsBase
    {
        [Fact]
        public void Constructor_ShouldGenerateNewGuid_WhenNoGuidPassed()
        {
            // Arrange & Act
            var entity = new TestEntityBase();

            // Assert
            entity.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenEntitiesHaveSameId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity1 = new TestEntityBase(id);
            var entity2 = new TestEntityBase(id);

            // Act
            var result = entity1.Equals(entity2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenEntitiesHaveDifferentIds()
        {
            // Arrange
            var entity1 = new TestEntityBase(Guid.NewGuid());
            var entity2 = new TestEntityBase(Guid.NewGuid());

            // Act
            var result = entity1.Equals(entity2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameHash_WhenEntitiesHaveSameId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity1 = new TestEntityBase(id);
            var entity2 = new TestEntityBase(id);

            // Act & Assert
            entity1.GetHashCode().Should().Be(entity2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentHash_WhenEntitiesHaveDifferentIds()
        {
            // Arrange
            var entity1 = new TestEntityBase(Guid.NewGuid());
            var entity2 = new TestEntityBase(Guid.NewGuid());

            // Act & Assert
            entity1.GetHashCode().Should().NotBe(entity2.GetHashCode());
        }
    }
}
