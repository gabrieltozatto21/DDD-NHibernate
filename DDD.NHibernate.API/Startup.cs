using AutoMapper;
using DDD.NHibernate.Aplicacao.Despesas.Profiles;
using DDD.NHibernate.Aplicacao.Despesas.Servicos;
using DDD.NHibernate.Dominio.Despesas.Servicos;
using DDD.NHibernate.Infra.Despesas.Mapeamentos;
using DDD.NHibernate.Infra.Despesas.Repositorios;
using DDD.NHibernate.Libs.Aplicacao.Transacoes.Interfaces;
using DDD.NHibernate.Libs.Core.Api.Filters;
using DDD.NHibernate.Libs.Core.Api.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using DDD.NHibernate.Libs.NHibernate.Transacoes;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NHibernate;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.HttpOverrides;

namespace DDD.NHibernate.API
{
    /// <summary>
    /// Classe Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Construtor Startup
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        private IConfiguration configuration { get; }
        private IHostingEnvironment env { get; }

        /// <summary>
        /// Configuração de serviços
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(config =>
            {
                config.Filters.Add<ExcecaoFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DDD.NHibernate.API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<DefaultOperationFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
                c.UseInlineDefinitionsForEnums();
            });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Secret"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "DDD.NHibernate.API-" + env.EnvironmentName);
                c.DisplayRequestDuration();
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");

            app.UseRewriter(option);

            app.UseHttpsRedirection();
        }
    }
}
