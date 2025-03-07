using CadavizCodeHub.WebApi.Setup;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CadavizCodeHub.WebApi
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        protected Program() { }

        private static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            var app = builder.Build()!;

            Startup.Configure(app);

            await app.RunAsync();
        }
    }
}