using AutoMapper;
using CrystalQuartz.AspNetCore;
using DDD.Nhibernate.Jobs.Jobs.DespesasJobs;
using DDD.Nhibernate.Jobs.Jobs.NotificacaoJob;
using DDD.NHibernate.Aplicacao.Despesas.Profiles;
using DDD.NHibernate.Aplicacao.Despesas.Servicos;
using DDD.NHibernate.Dominio.Despesas.Servicos;
using DDD.NHibernate.Infra.Despesas.Mapeamentos;
using DDD.NHibernate.Infra.Despesas.Repositorios;
using DDD.NHibernate.Libs.Aplicacao.Transacoes.Interfaces;
using DDD.NHibernate.Libs.Core.Jobs.Extensions;
using DDD.NHibernate.Libs.Core.Jobs.Factorys;
using DDD.NHibernate.Libs.Core.Jobs.Listeners;
using DDD.NHibernate.Libs.NHibernate.Transacoes;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using Serilog;
using System.Reflection;

namespace DDD.Nhibernate.Jobs
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment env { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, ScheduledJobFactory>();
            services.AddSingleton<IJobListener, LogsJobListener>();

            services.AddJobs();

            services.AddSingleton<IScheduler>(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();

                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();

                scheduler.ListenerManager.AddJobListener(provider.GetService<IJobListener>(), GroupMatcher<JobKey>.AnyGroup());

                var despesaValorJob = JobBuilder.Create<DespesaValorJob>().WithIdentity("DespesaValorJob", "Despesa").WithDescription("Executa a alteração de valor da despesa").StoreDurably().Build();
                scheduler.ScheduleJob(despesaValorJob, TriggerBuilder.Create().WithCronSchedule("20 0 0 ? * * *").Build());

                var notificacaoJob = JobBuilder.Create<NotificacaoJob>().WithIdentity("NotificacaoJov", "Notificacao").WithDescription("Verifica se há notificacao pendente").StoreDurably().Build();
                scheduler.ScheduleJob(notificacaoJob, TriggerBuilder.Create().WithCronSchedule("20 0 0 ? * * *").Build());

                scheduler.Start();

                if (env.EnvironmentName != "Prod")
                {
                    scheduler.PauseAll();
                }

                return scheduler;
            });

            services.AddSingleton<ISessionFactory>(factory =>
            {
                string connectionString = Configuration.GetConnectionString("mysql");

                ISessionFactory sessionFactory = Fluently.Configure().Database(
                        MySQLConfiguration.Standard
                        .FormatSql()
                        .ShowSql()
                        .ConnectionString(connectionString)
                    )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DespesaMap>())
                    .CurrentSessionContext("call")
                    .BuildSessionFactory();

                return sessionFactory;
            }
            );

            services.AddScoped<ISession>(factory =>
            {
                return factory.GetService<ISessionFactory>().OpenSession();
            }
            );


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(DespesaProfile).GetTypeInfo().Assembly);

            services.Scan(scan => scan
                .FromAssemblyOf<DespesaAppServico>()
                    .AddClasses()
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<DespesaServico>()
                    .AddClasses()
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<DespesaRepositorio>()
                    .AddClasses()
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IScheduler scheduler, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddSerilog();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseCrystalQuartz(() => scheduler);

            var option = new RewriteOptions();
            option.AddRedirect("^$", "quartz");

            app.UseRewriter(option);
        }
    }
}
