using DDD.NHibernate.Libs.Core.Api.Logs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Extensions.Logging;
using System;

namespace DDD.NHibernate.Libs.Core.Api.Hosting
{
    public static class WebhostBuilderExtensions
    {
        public static IWebHostBuilder UseSerilog(this IWebHostBuilder builder,
            Action<IServiceProvider, WebHostBuilderContext, LoggerConfiguration> configureLogger,
            bool preserveStaticLogger = false)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (configureLogger == null)
                throw new ArgumentNullException(nameof(configureLogger));

            builder.ConfigureServices((context, collection) =>
            {
                collection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                var provider = collection.BuildServiceProvider();
                var hca = provider.GetRequiredService<IHttpContextAccessor>();

                collection.AddSingleton<AspNetCoreHttpContextEnricher>();

                LoggerConfiguration loggerConfiguration = new LoggerConfiguration();
                configureLogger(collection.BuildServiceProvider(), context, loggerConfiguration);

                Logger logger = loggerConfiguration.CreateLogger();
                if (preserveStaticLogger)
                {
                    collection.AddSingleton(services => (ILoggerFactory)new SerilogLoggerFactory(logger, true));
                }
                else
                {
                    Log.Logger = logger;
                    collection.AddSingleton(services => (ILoggerFactory)new SerilogLoggerFactory(null, true));
                }
            });
            return builder;
        }
    }
}
