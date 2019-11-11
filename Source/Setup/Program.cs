using System;
using Autofac;
using Dolittle.Booting;
using Dolittle.DependencyInversion;
using Dolittle.DependencyInversion.Autofac;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Tenancy;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Dolittle.Collections;

namespace Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new LogAppender();
            logger.MinimumLevel = LogLevel.Warning;

            var bootResult = Bootloader.Configure(_ => {
                _.UseLogAppender(logger);
            }).Start();

            var container = bootResult.Container;
            var manager = container.Get<IExecutionContextManager>();
            manager.CurrentFor(TenantId.Development);
            var command = container.Get<Command>();
            logger.MinimumLevel = LogLevel.Trace;
            command.Run();
        }
    }

    public class LogAppender : ILogAppender
    {
        public LogLevel MinimumLevel { get; set; } = LogLevel.Info;

        public void Append(string filePath, int lineNumber, string member, LogLevel level, string message, Exception exception = null)
        {
            if (level >= MinimumLevel)
            {
                Console.WriteLine($"[{level}] {message}");
            }
        }
    }
}
