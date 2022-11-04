using DDD.NHibernate.Aplicacao.UsuariosAcesso.Servicos.Interfaces;
using DDD.NHibernate.Aplicacao.UsuariosNotificacoes.Servicos.Interfaces;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Request;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Nhibernate.Jobs.Jobs.NotificacaoJob
{
    public class NotificacaoJob : IJob
    {
        private readonly ILogger<NotificacaoJob> logger;
        private readonly IUsuariosNotificacoesAppServico usuariosNotificacoesAppServico;
        private IUsuariosAcessoAppServico usuariosAcessoAppServico;
        private readonly IConfiguration configuration;

        public NotificacaoJob(
            IUsuariosNotificacoesAppServico usuariosNotificacoesAppServico,
            ILogger<NotificacaoJob> logger,
            IUsuariosAcessoAppServico usuariosAcessoAppServico,
            IConfiguration configuration)
        {
            this.usuariosNotificacoesAppServico = usuariosNotificacoesAppServico;
            this.logger = logger;
            this.usuariosAcessoAppServico = usuariosAcessoAppServico;
            this.configuration = configuration;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (LogContext.PushProperty("TransactionId", context?.FireInstanceId ?? Guid.NewGuid().ToString()))
            using (LogContext.PushProperty("Job", context?.JobDetail.JobType.Name ?? GetType().FullName))
            {
                try
                {
                    UsuarioAcessoAutenticacaoRequest acesso = new();

                    acesso.Login = "string";
                    acesso.Senha = "string";
                    var autenticacao = usuariosAcessoAppServico.Autenticar(acesso);

                    if (autenticacao != null)
                    {
                        HubConnection connection = new HubConnectionBuilder()
                        .WithUrl($"{configuration.GetSection("SignalR:Hub").Value}")
                        .Build();

                        var t = connection.StartAsync();
                        t.Wait();

                        await usuariosNotificacoesAppServico.DispararNotificacoes(connection);

                        var t3 = connection.StopAsync();
                        t3.Wait();
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError("<{EventoId:l}> {DecricaoGenericaErro} {DescricaoErro} {InnerException} {StackTrace}", "DispararNotificacoesJob", "Erro ao disparar notificações via SignalR para os clientes conectados", ex.Message, ex.InnerException, ex.StackTrace);
                }
            }
        }
    }
}
