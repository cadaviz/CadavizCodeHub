using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace CadavizCodeHub.WebApi.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class SerializationSetup
    {
        public static IMvcBuilder ConfigureSerialization(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            return builder;
        }
    }
}
