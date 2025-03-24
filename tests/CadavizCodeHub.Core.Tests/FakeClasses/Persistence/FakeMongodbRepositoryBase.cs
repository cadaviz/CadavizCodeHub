using CadavizCodeHub.Core.Persistence.MongoDB.Repositories;
using CadavizCodeHub.Core.Persistence.Setup;
using CadavizCodeHub.Core.Tests.FakeClasses.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace CadavizCodeHub.Core.Tests.FakeClasses.Persistence
{
    public class FakeMongodbRepositoryBase : MongodbRepositoryBase<FakeEntityBase>
    {
        protected override string CollectionName => nameof(FakeEntityBase);

        public FakeMongodbRepositoryBase(DatabaseSettings databaseSettings, IMongoClient mongoClient, ILogger<FakeMongodbRepositoryBase> logger)
            : base(databaseSettings, mongoClient, logger) { }

        public string CollectionNameForTest => CollectionName;
    }
}
