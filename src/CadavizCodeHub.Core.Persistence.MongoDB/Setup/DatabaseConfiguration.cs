using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Core.Persistence.MongoDB.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class DatabaseConfiguration
    {
        public static void RegisterSerializer()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
    }
}
