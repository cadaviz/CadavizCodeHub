using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using CadavizCodeHub.Domain.Repositories;
using CadavizCodeHub.Framework.Domain;
using MongoDB.Driver;

namespace CadavizCodeHub.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    internal abstract class MongodbRepositoryBase<T> : IDisposable
        where T : class, IEntity
    {
        private bool _disposed = false;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        protected readonly IMongoCollection<T> _collection;

        protected MongodbRepositoryBase(DatabaseSettings databaseSettings, string collectionName)
        {
            _client = new MongoClient(databaseSettings.ConnectionString);
            ArgumentNullException.ThrowIfNull(_client);

            _database = _client.GetDatabase(databaseSettings.DatabaseName);
            ArgumentNullException.ThrowIfNull(_database);

            _collection = _database.GetCollection<T>(collectionName);
            ArgumentNullException.ThrowIfNull(_collection);
        }

        protected Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _collection.Find(filter => filter.Id == id)
                              .SingleOrDefaultAsync(cancellationToken);
        }

        protected async Task<IEnumerable<T>> GetByFilterAsync(FilterDefinition<T> filter, CancellationToken cancellationToken)
        {
            return (await _collection.FindAsync(filter, cancellationToken: cancellationToken)).ToEnumerable(cancellationToken: cancellationToken);
        }

        protected Task CreateAsync(T entity, CancellationToken cancellationToken)
        {
            return _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _client.Dispose();
                _database.Client.Dispose();
            }

            _disposed = true;
        }

        ~MongodbRepositoryBase()
        {
            Dispose(false);
        }
    }
}
