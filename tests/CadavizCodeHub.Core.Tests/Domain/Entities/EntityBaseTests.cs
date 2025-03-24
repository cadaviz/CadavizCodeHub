using CadavizCodeHub.Core.Tests.FakeClasses.Domain;
using CadavizCodeHub.Tests.Shared.Shared;
using FluentAssertions;
using System;
using Xunit;

namespace CadavizCodeHub.Core.Tests.Domain.Entities
{
    public class EntityBaseTests : TestBase
    {
        [Fact]
        public void Constructor_ShouldGenerateNewGuid_WhenNoGuidPassed()
        {
            // Arrange & Act
            var entity = new FakeEntityBase();

            // Assert
            entity.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenEntitiesHaveSameId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity1 = new FakeEntityBase(id);
            var entity2 = new FakeEntityBase(id);

            // Act
            var result = entity1.Equals(entity2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenEntitiesHaveDifferentIds()
        {
            // Arrange
            var entity1 = new FakeEntityBase(Guid.NewGuid());
            var entity2 = new FakeEntityBase(Guid.NewGuid());

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
            var entity1 = new FakeEntityBase(id);
            var entity2 = new FakeEntityBase(id);

            // Act & Assert
            entity1.GetHashCode().Should().Be(entity2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentHash_WhenEntitiesHaveDifferentIds()
        {
            // Arrange
            var entity1 = new FakeEntityBase(Guid.NewGuid());
            var entity2 = new FakeEntityBase(Guid.NewGuid());

            // Act & Assert
            entity1.GetHashCode().Should().NotBe(entity2.GetHashCode());
        }
    }
}
