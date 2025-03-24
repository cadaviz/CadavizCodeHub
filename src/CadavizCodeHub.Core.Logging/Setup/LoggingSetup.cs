using CadavizCodeHub.Core.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Core.Logging.Setup
{
    [ExcludeFromCodeCoverage]
    public static class LoggingSetup
    {
        public static IServiceCollection ConfigureLogs(this IServiceCollection services, IConfiguration configuration)
        {
            var loggingSettings = configuration.GetSection("LoggingSettings").Get<LoggingSettings>();

            ArgumentNullException.ThrowIfNull(loggingSettings);

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is(EnumExtensions.Convert<LogLevel, LogEventLevel>(loggingSettings.MinimumLogLevel, LogEventLevel.Information))
                .Enrich.FromLogContext()
                ;

            if (loggingSettings.WriteLogTo.HasFlag(LogDestinations.Console))
            {
                loggerConfiguration.WriteTo.Console(outputTemplate: loggingSettings.OutputTemplate);
            }

            if (loggingSettings.WriteLogTo.HasFlag(LogDestinations.File))
            {
                loggerConfiguration.WriteTo.File(
                    path: loggingSettings.FileSettings.Path,
                    outputTemplate: loggingSettings.OutputTemplate,
                    rollingInterval: EnumExtensions.Convert<LogRollingInterval, RollingInterval>(loggingSettings.FileSettings.RollingInterval, RollingInterval.Day),
                    retainedFileCountLimit: loggingSettings.FileSettings.RetainedFileCountLimit);
            }

            Log.Logger = loggerConfiguration.CreateLogger();

            services.AddSingleton(Log.Logger);
            services.AddSingleton<DiagnosticContext>();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });

            return services;
        }

        public static IApplicationBuilder ConfigureLogs(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();

            return app;
        }
    }
}