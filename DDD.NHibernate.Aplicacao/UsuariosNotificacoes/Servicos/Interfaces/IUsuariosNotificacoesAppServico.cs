using DDD.NHibernate.DataTransfer.UsuariosNotificacoes.Request;
using DDD.NHibernate.DataTransfer.UsuariosNotificacoes.Response;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Aplicacao.UsuariosNotificacoes.Servicos.Interfaces
{
    public interface IUsuariosNotificacoesAppServico
    {
        Task DispararNotificacoes(HubConnection conexao);
        IList<UsuarioNotificacoesResponse> Notificar(UsuarioNotificacaoRequest request);
    }
}
