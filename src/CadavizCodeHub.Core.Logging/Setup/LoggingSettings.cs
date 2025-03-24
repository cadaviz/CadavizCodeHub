using System;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Core.Logging.Setup
{
    [ExcludeFromCodeCoverage]
    public class LoggingSettings
    {
        public WriteLogTo WriteLogTo { get; set; } = WriteLogTo.Console;
        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;
        public string OutputTemplate { get; set; } = "[{Level:u3}][{Timestamp:HH:mm:ss}] Message:{Message}{NewLine}Properties:{Properties:j} at {SourceContext}{NewLine}{Exception:lj}";
        public LoggingToFileSettings FileSettings { get; set; } = new LoggingToFileSettings();
    }

    [ExcludeFromCodeCoverage]
    public class LoggingToFileSettings
    {
        public string Path { get; set; } = "../../logs/log.txt";
        public LogRollingInterval RollingInterval { get; set; } = LogRollingInterval.Day;
        public int RetainedFileCountLimit { get; set; } = 7;
    }

    [Flags]
    public enum WriteLogTo
    {
        None = 0,
        Console = 1,
        File = 2,
    }

    public enum LogLevel
    {
        Verbose,
        Debug,
        Information,
        Warning,
        Error,
        Fatal,
    }

    public enum LogRollingInterval
    {
        Infinite,
        Year,
        Month,
        Day,
        Hour,
        Minute
    }
}