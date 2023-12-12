using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadavizCodeHub.Domain.Repositories;
using CadavizCodeHub.Framework.Domain;
using MongoDB.Driver;

namespace CadavizCodeHub.Infrastructure.Repositories
{
    internal abstract class MongodbRepositoryBase<T> : IDisposable
        where T : IEntity
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        protected readonly IMongoCollection<T> _collection;

        protected MongodbRepositoryBase(DatabaseSettings databaseSettings, string collectionName)
        {
            _client = new MongoClient(databaseSettings.ConnectionString);
            _database = _client.GetDatabase(databaseSettings.DatabaseName);
            _collection = _database.GetCollection<T>(collectionName);
        }

        protected Task<T> GetByIdAsync(Guid id)
        {
            return _collection.Find(filter => filter.Id == id)
                              .SingleOrDefaultAsync();
        }

        protected async Task<IEnumerable<T>> GetByFilterAsync(FilterDefinition<T> filter)
        {
            return (await _collection.FindAsync(filter)).ToEnumerable();
        }

        protected Task CreateAsync(T entity)
        {
            return _collection.InsertOneAsync(entity);
        }

        public void Dispose()
        {
        }
    }
}
