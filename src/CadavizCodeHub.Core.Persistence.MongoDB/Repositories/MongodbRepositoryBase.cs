using CadavizCodeHub.Core.Domain.Entities;
using CadavizCodeHub.Core.Domain.Repositories;
using CadavizCodeHub.Core.Logging.Extensions;
using CadavizCodeHub.Core.Persistence.Setup;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CadavizCodeHub.Core.Persistence.MongoDB.Repositories
{
    public abstract class MongodbRepositoryBase<T> : ICrudRepositoryBase<T>
        where T : class, IEntity
    {
        protected readonly IMongoCollection<T> _collection;
        protected readonly ILogger<MongodbRepositoryBase<T>> _logger;

        protected MongodbRepositoryBase(DatabaseSettings databaseSettings, IMongoClient mongoClient, string collectionName, ILogger<MongodbRepositoryBase<T>> logger)
        {
            ArgumentNullException.ThrowIfNull(databaseSettings);
            ArgumentNullException.ThrowIfNull(mongoClient);
            ArgumentNullException.ThrowIfNull(collectionName);
            ArgumentNullException.ThrowIfNull(logger);

            var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            ArgumentNullException.ThrowIfNull(database);

            _collection = database.GetCollection<T>(collectionName);
            ArgumentNullException.ThrowIfNull(_collection);

            _logger = logger;
        }

        public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _collection.Find(filter => filter.Id == id)
                              .SingleOrDefaultAsync(cancellationToken) as Task<T?>;
        }

        internal protected async Task<IReadOnlyCollection<T>> GetByFilterAsync(FilterDefinition<T> filter, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);
            var result = cursor == null ? new List<T>() : await cursor.ToListAsync(cancellationToken);

            _logger.LogDebugIfEnabled("Getting entities by filter found this result. Filter='{Filter}' Result='{Result}'", filter, result);

            return result.AsReadOnly(); 
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);

            _logger.LogDebugIfEnabled("Entity was inserted. Entity='{Entity}'", entity);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);

            _logger.LogDebugIfEnabled("Entity was updated. Entity='{Entity}'", entity);

            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id, cancellationToken);

            _logger.LogDebugIfEnabled("Entity with {Id} was deleted. Result='{Result}'", id, result);

            return result.DeletedCount > 0;
        }
    }
}
