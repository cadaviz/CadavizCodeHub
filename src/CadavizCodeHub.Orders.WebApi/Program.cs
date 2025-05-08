using CadavizCodeHub.Orders.WebApi.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CadavizCodeHub.Orders.WebApi
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

            bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out bool runningInContainer);

            if (runningInContainer)
            {
                builder.WebHost.UseKestrel();

            }

            var app = builder.Build()!;

            Startup.Configure(app);

            await app.RunAsync();
        }
    }
}