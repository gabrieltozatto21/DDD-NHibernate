using DDD.NHibernate.Aplicacao.Despesas.Servicos;
using DDD.NHibernate.Aplicacao.Despesas.Servicos.Interfaces;
using DDD.NHibernate.Dominio.Despesas.Repositorios;
using DDD.NHibernate.Dominio.Despesas.Servicos;
using DDD.NHibernate.Dominio.Despesas.Servicos.Interfaces;
using DDD.NHibernate.Infra.Despesas.Mapeamentos;
using DDD.NHibernate.Infra.Despesas.Repositorios;
using DDD.NHibernate.Libs.Aplicacao.Transacoes.Interfaces;
using DDD.NHibernate.Libs.NHibernate.Transacoes;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DDD.NHibernate.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfiguration configuration { get; }
        private readonly IHostingEnvironment env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<ISessionFactory>(factory =>
                {
                    string connectionString = configuration.GetConnectionString("mysql");

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
            services.AddScoped<IDespesaAppServico, DespesaAppServico>();
            services.AddScoped<IDespesaServico, DespesaServico>();
            services.AddScoped<IDespesaRepositorio, DespesaRepositorio>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
