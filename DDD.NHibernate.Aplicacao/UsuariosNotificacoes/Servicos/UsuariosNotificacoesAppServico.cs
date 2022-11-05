using DDD.NHibernate.Aplicacao.UsuariosNotificacoes.Servicos.Interfaces;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Repositorios;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Servicos.Interfaces;
using DDD.NHibernate.Libs.Aplicacao.Transacoes.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Aplicacao.UsuariosNotificacoes.Servicos
{
    public class UsuariosNotificacoesAppServico : IUsuariosNotificacoesAppServico
    {

        private readonly IUsuariosNotificacoesServico usuariosNotificacoesServico;
        private readonly IUsuariosNotificacoesRepositorio usuariosNotificacoesRepositorio;
        private readonly IUnitOfWork unitOfWork;

        public UsuariosNotificacoesAppServico(
            IUsuariosNotificacoesServico usuariosNotificacoesServico,
            IUsuariosNotificacoesRepositorio usuariosNotificacoesRepositorio,
            IUnitOfWork unitOfWork
            )
        {
            this.usuariosNotificacoesServico = usuariosNotificacoesServico;
            this.usuariosNotificacoesRepositorio = usuariosNotificacoesRepositorio;
            this.unitOfWork = unitOfWork;
        }

        public async Task DispararNotificacoes(HubConnection conexao)
        {
            var usuariosNotificacoes = usuariosNotificacoesRepositorio.ListarTodos()
                .Where(s => !s.Visualizado)
                //.Where(x => (x.Notificacao.DataExibicao <= DateTime.Now || x.Notificacao.DataExibicao == null) && x.Notificacao.Ativo)
                .Where(x => (x.Notificacao.DataExibicao <= DateTime.Now || x.Notificacao.DataExibicao == null) && x.Notificacao.Ativo)
                .ToList();

            foreach (UsuarioNotificacao usuarioNotificacao in usuariosNotificacoes)
            {
                await conexao.InvokeAsync("NotificarTodos", 
                    usuarioNotificacao.Notificacao.Descricao);
            }
        }
    }
}
