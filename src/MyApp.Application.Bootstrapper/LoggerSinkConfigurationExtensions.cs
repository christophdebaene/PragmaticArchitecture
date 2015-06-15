using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Formatting.Json;
using Serilog.Sinks.RollingFile;
using System;

namespace Serilog
{
    public static class LoggerSinkConfigurationExtensions
    {
        private const long DefaultFileSizeLimitBytes = 1L * 1024 * 1024 * 1024; // 1Gigabyte
        private const int DefaultRetainedFileCountLimit = 31;
        private const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:l}{NewLine:l}{Exception:l}";

        public static LoggerConfiguration RollingFileAsJson(this LoggerSinkConfiguration sinkConfiguration, string pathFormat, LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (sinkConfiguration == null)
                throw new ArgumentNullException("sinkConfiguration");

            var jsonSink = new RollingFileSink(pathFormat, new JsonFormatter(false, null, true), DefaultFileSizeLimitBytes, DefaultRetainedFileCountLimit);
            return sinkConfiguration.Sink(jsonSink, restrictedToMinimumLevel);
        }

        public static LoggerConfiguration RollingFileAsText(this LoggerSinkConfiguration sinkConfiguration, string pathFormat, LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (sinkConfiguration == null)
                throw new ArgumentNullException("sinkConfiguration");

            var formatter = new MessageTemplateTextFormatter(DefaultOutputTemplate, null);
            var sink = new RollingFileSink(pathFormat, formatter, DefaultFileSizeLimitBytes, DefaultRetainedFileCountLimit);
            return sinkConfiguration.Sink(sink, restrictedToMinimumLevel);
        }
    }
}