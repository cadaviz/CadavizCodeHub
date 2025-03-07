using CadavizCodeHub.Domain.Repositories;
using CadavizCodeHub.Framework.Domain;
using CadavizCodeHub.Persistence.Database;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CadavizCodeHub.Persistence.Repositories
{
    [ExcludeFromCodeCoverage]
    internal abstract class MongodbRepositoryBase<T> : ICrudRepositoryBase<T>
        where T : class, IEntity
    {
        protected readonly IMongoCollection<T> _collection;

        protected MongodbRepositoryBase(DatabaseSettings databaseSettings, IMongoClient mongoClient, string collectionName)
        {
            ArgumentNullException.ThrowIfNull(databaseSettings);
            ArgumentNullException.ThrowIfNull(mongoClient);
            ArgumentNullException.ThrowIfNull(collectionName);

            var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            ArgumentNullException.ThrowIfNull(database);

            _collection = database.GetCollection<T>(collectionName);
            ArgumentNullException.ThrowIfNull(_collection);
        }

        public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _collection.Find(filter => filter.Id == id)
                              .SingleOrDefaultAsync(cancellationToken) as Task<T?>;
        }

        internal protected async Task<IReadOnlyCollection<T>> GetByFilterAsync(FilterDefinition<T> filter, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);
            return cursor == null ? Array.Empty<T>() : await cursor.ToListAsync(cancellationToken);
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);

            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
            return result.DeletedCount > 0;
        }
    }
}
