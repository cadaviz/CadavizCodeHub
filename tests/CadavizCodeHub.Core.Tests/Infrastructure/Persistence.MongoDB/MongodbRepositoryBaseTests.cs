using AutoFixture;
using CadavizCodeHub.Core.Persistence.MongoDB.Repositories;
using CadavizCodeHub.Core.Persistence.Setup;
using CadavizCodeHub.Tests.Shared.TestClasses.Domain;
using CadavizCodeHub.Tests.Shared.Tools;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CadavizCodeHub.Persistence.UnitTests.Repositories
{
    public class MongodbRepositoryBaseTests : TestsBase
    {
        private readonly Mock<IMongoCollection<TestEntityBase>> _mockCollection;
        private readonly MongodbRepositoryBase<TestEntityBase> _repository;

        public MongodbRepositoryBaseTests()
        {
            var databaseSettings = Fixture.Create<DatabaseSettings>();
            var _mockMongoClient = new Mock<IMongoClient>();
            var _mockDatabase = new Mock<IMongoDatabase>();
            var _loggerMock = new Mock<ILogger<MongodbRepositoryBaseTest>>();

            _mockCollection = new Mock<IMongoCollection<TestEntityBase>>();

            _mockMongoClient.Setup(x => x.GetDatabase(It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>()))
                .Returns(_mockDatabase.Object);

            _mockDatabase.Setup(x => x.GetCollection<TestEntityBase>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                         .Returns(_mockCollection.Object);

            _repository = new MongodbRepositoryBaseTest(databaseSettings, _mockMongoClient.Object, nameof(TestEntityBase), _loggerMock.Object);
        }
        [Fact]
        public async Task CreateAsync_ShouldInsertEntity()
        {
            // Arrange
            var expectedEntity = new TestEntityBase();

            _mockCollection.Setup(x => x.InsertOneAsync(expectedEntity, It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _repository.CreateAsync(expectedEntity, CancellationToken.None);

            // Assert
            expectedEntity.Should().BeEquivalentTo(result);

            _mockCollection.Verify(
                x => x.InsertOneAsync(
                    It.Is<TestEntityBase>(e => e == expectedEntity),
                    It.IsAny<InsertOneOptions>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            // Arrange
            var expectedEntity = new TestEntityBase();

            var replaceResult = new ReplaceOneResult.Acknowledged(1, 1, new BsonBinaryData(expectedEntity.Id, GuidRepresentation.Standard));

            _mockCollection
                .Setup(x => x.ReplaceOneAsync(
                    It.Is<FilterDefinition<TestEntityBase>>(f => f != null),
                    expectedEntity,
                    It.IsAny<ReplaceOptions>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(replaceResult);

            // Act
            var result = await _repository.UpdateAsync(expectedEntity, CancellationToken.None);

            // Assert
            expectedEntity.Should().BeEquivalentTo(result);

            _mockCollection.Verify(
                x => x.ReplaceOneAsync(
                    It.Is<FilterDefinition<TestEntityBase>>(f => f != null),
                    expectedEntity,
                    It.IsAny<ReplaceOptions>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var deleteResult = new DeleteResult.Acknowledged(1);

            _mockCollection
                .Setup(x => x.DeleteOneAsync(
                    It.Is<FilterDefinition<TestEntityBase>>(f => f != null),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(deleteResult);

            // Act
            var result = await _repository.DeleteAsync(entityId, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            _mockCollection.Verify(
                x => x.DeleteOneAsync(
                    It.Is<FilterDefinition<TestEntityBase>>(f => f != null),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenEntityDoesNotExist()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var deleteResult = new DeleteResult.Acknowledged(0);

            _mockCollection
                .Setup(x => x.DeleteOneAsync(
                    It.Is<FilterDefinition<TestEntityBase>>(f => f != null),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(deleteResult);

            // Act
            var result = await _repository.DeleteAsync(entityId, CancellationToken.None);

            // Assert
            result.Should().BeFalse();

            _mockCollection.Verify(
                x => x.DeleteOneAsync(
                    It.Is<FilterDefinition<TestEntityBase>>(f => f != null),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
        {
            // Arrange
            var expectedEntity = new TestEntityBase();
            var expectedResult = new List<TestEntityBase> { expectedEntity };

            var mockCursor = MockCursor(expectedResult);

            _mockCollection
                .Setup(x => x.FindAsync(It.IsAny<FilterDefinition<TestEntityBase>>(),
                                        It.IsAny<FindOptions<TestEntityBase>>(),
                                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _repository.GetByIdAsync(expectedEntity.Id, CancellationToken.None);

            // Assert
            expectedEntity.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenEntityDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            var expectedResult = Enumerable.Empty<TestEntityBase>();

            var mockCursor = MockCursor(expectedResult);

            _mockCollection
                .Setup(x => x.FindAsync(It.IsAny<FilterDefinition<TestEntityBase>>(),
                                        It.IsAny<FindOptions<TestEntityBase>>(),
                                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _repository.GetByIdAsync(id, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByFilterAsync_ShouldReturnEntities_WhenEntitiesExist()
        {
            var entity1 = new TestEntityBase();
            var entity2 = new TestEntityBase();
            var expectedResult = new List<TestEntityBase> { entity1, entity2 };

            var filter = Builders<TestEntityBase>.Filter.Empty;

            var mockCursor = MockCursor(expectedResult);

            _mockCollection
                .Setup(x => x.FindAsync(It.IsAny<FilterDefinition<TestEntityBase>>(),
                                        It.IsAny<FindOptions<TestEntityBase>>(),
                                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _repository.GetByFilterAsync(filter, CancellationToken.None);

            // Assert
            result.Should().HaveCount(expectedResult.Count)
                  .And.ContainInOrder(expectedResult);
        }

        [Fact]
        public async Task GetByFilterAsync_ShouldReturnEntities_WhenEntitiesDoesNotExist()
        {
            var expectedResult = Enumerable.Empty<TestEntityBase>();

            var filter = Builders<TestEntityBase>.Filter.Empty;

            var mockCursor = MockCursor(expectedResult);

            _mockCollection
                .Setup(x => x.FindAsync(It.IsAny<FilterDefinition<TestEntityBase>>(),
                                        It.IsAny<FindOptions<TestEntityBase>>(),
                                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _repository.GetByFilterAsync(filter, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        private static Mock<IAsyncCursor<T>> MockCursor<T>(IEnumerable<T> expectedValues)
        {
            var _mockCursor = new Mock<IAsyncCursor<T>>();

            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                             .Returns(true)
                             .Returns(false);

            _mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true)
                       .ReturnsAsync(false);

            _mockCursor.Setup(x => x.Current)
                       .Returns(expectedValues);

            return _mockCursor;
        }
    }

    public class MongodbRepositoryBaseTest : MongodbRepositoryBase<TestEntityBase>
    {
        public MongodbRepositoryBaseTest(DatabaseSettings databaseSettings, IMongoClient mongoClient, string collectionName, ILogger<MongodbRepositoryBaseTest> logger) : base(databaseSettings, mongoClient, collectionName, logger) { }
    }
}
