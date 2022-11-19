using AutoMapper;
using DDD.NHibernate.Aplicacao.UsuariosNotificacoes.Servicos.Interfaces;
using DDD.NHibernate.DataTransfer.UsuariosNotificacoes.Request;
using DDD.NHibernate.DataTransfer.UsuariosNotificacoes.Response;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Repositorios;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Servicos.Interfaces;
using DDD.NHibernate.Dominio.UsuariosAcesso.Servicos.Interfaces;
using DDD.NHibernate.Libs.Aplicacao.Transacoes.Interfaces;
using DDD.NHibernate.Libs.Core.Api.Usuarios.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using NHibernate.Mapping.ByCode.Impl;
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
        private readonly IUsuario usuario;
        private readonly IUsuarioAcessoServico usuarioServico;
        private readonly IMapper mapper;

        public UsuariosNotificacoesAppServico(
            IUsuariosNotificacoesServico usuariosNotificacoesServico,
            IUsuariosNotificacoesRepositorio usuariosNotificacoesRepositorio,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUsuario usuario,
            IUsuarioAcessoServico usuarioServico)
        {
            this.usuariosNotificacoesServico = usuariosNotificacoesServico;
            this.usuariosNotificacoesRepositorio = usuariosNotificacoesRepositorio;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.usuario = usuario;
            this.usuarioServico = usuarioServico;
        }

        public async Task DispararNotificacoes(HubConnection conexao)
        {
            try
            {
                unitOfWork.BeginTransaction();

                var usuariosNotificacoes = usuariosNotificacoesRepositorio.ListarTodos()
                    .Where(s => !s.Visualizado)
                    .Where(x => !x.Notificado)
                    .Where(x => (x.Notificacao.DataExibicao <= DateTime.Now || x.Notificacao.DataExibicao == null) )
                    .Where(x => x.Notificacao.Ativo)
                    .ToList();

                foreach (UsuarioNotificacao usuarioNotificacao in usuariosNotificacoes)
                {
                    await conexao.InvokeAsync("NotificarTodos",
                        "{\"Mensagem\": " + usuarioNotificacao.Id.ToString() +
                        ",\"IdUsuario\": " + usuarioNotificacao.Usuario.Id +
                        "}");

                }

                unitOfWork.Commit();

            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public IList<UsuarioNotificacoesResponse> Notificar(UsuarioNotificacaoRequest request)
        {
            int idUsuario = int.Parse(usuario.Id);

            var usuarioAcesso = this.usuarioServico.Validar(idUsuario);

            var query = usuariosNotificacoesRepositorio.ListarTodos()
                .Where(s => s.Usuario.Id == usuarioAcesso.Id)
                .Where(s => !s.Visualizado)
                .Where(x => (x.Notificacao.DataExibicao <= DateTime.Now || x.Notificacao.DataExibicao == null))
                .Where(x => x.Notificacao.Ativo);

            var usuariosNotificacoes = query.ToList();

            var response = mapper.Map<IList<UsuarioNotificacoesResponse>>(usuariosNotificacoes);

            try
            {
                unitOfWork.BeginTransaction();

                foreach (var usuarioNotificacao in usuariosNotificacoes)
                {
                    usuarioNotificacao.SetNotificado(true);
                    usuariosNotificacoesRepositorio.Editar(usuarioNotificacao);
                }

                unitOfWork.Commit();
            }
            catch (Exception ex){
                throw ex;
            }
            

            return response;
        }
    }
}
