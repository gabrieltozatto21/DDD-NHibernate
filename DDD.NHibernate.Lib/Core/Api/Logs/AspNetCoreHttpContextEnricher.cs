using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace DDD.NHibernate.Libs.Core.Api.Logs
{
    public class AspNetCoreHttpContextEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AspNetCoreHttpContextEnricher(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            HttpContext currentHttpContext = httpContextAccessor.HttpContext;
            if (currentHttpContext == null) return;

            var httpContextCache = currentHttpContext.Items[$"serilog-enrichers-aspnetcore-httpcontext"];

            if (httpContextCache == null)
            {
                httpContextCache = StandardEnricher(httpContextAccessor, logEvent);
                currentHttpContext.Items[$"serilog-enrichers-aspnetcore-httpcontext"] = httpContextCache;
            }

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("HttpContext", httpContextCache, true));
        }

        public HttpContextCache StandardEnricher(IHttpContextAccessor hca, LogEvent logEvent)
        {
            var ctx = hca.HttpContext;
            if (ctx == null) return null;

            var httpContextCache = new HttpContextCache
            {
                TransactionId = ctx.Request.Headers["TransactionId"].SingleOrDefault(),
                Path = ctx.Request.Path.ToString(),
                Method = ctx.Request.Method,
                Headers = ctx.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString())
            };

            if (logEvent.Level == LogEventLevel.Error)
            {
                httpContextCache.QueryString = ctx.Request.QueryString.ToString();

                if (ctx.Request.ContentLength.HasValue && ctx.Request.ContentLength > 0)
                {
                    ctx.Request.EnableBuffering();

                    using (StreamReader reader = new StreamReader(ctx.Request.Body, Encoding.UTF8, true, 1024, true))
                    {
                        httpContextCache.Body = reader.ReadToEnd();
                    }

                    ctx.Request.Body.Position = 0;
                }
            }

            return httpContextCache;
        }
    }

    public static class LoggerEnrichmentConfigurationExtensions
    {
        public static LoggerConfiguration WithAspnetcoreHttpcontext(this LoggerEnrichmentConfiguration enrichmentConfiguration,
            IServiceProvider serviceProvider)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));

            var enricher = serviceProvider.GetService<AspNetCoreHttpContextEnricher>();


            return enrichmentConfiguration.With(enricher);
        }
    }
}
