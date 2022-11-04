using DDD.NHibernate.Dominio.Notificacoes.Servicos.Interfaces;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Repositorios;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Servicos.Interfaces;
using DDD.NHibernate.Dominio.UsuariosAcesso.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Dominio.UsuarioNotificacoes.Servicos
{
    public class UsuariosNotificacoesServico : IUsuariosNotificacoesServico
    {
        private readonly IUsuarioAcessoServico usuarioAcessoServico;
        private readonly INotificacoesServico notificacoesServico;
        private readonly IUsuariosNotificacoesRepositorio usuariosNotificacoesRepositorio;

        public UsuariosNotificacoesServico(
            IUsuarioAcessoServico usuarioAcessoServico,
            INotificacoesServico notificacoesServico,
            IUsuariosNotificacoesRepositorio usuariosNotificacoesRepositorio
            )
        {
            this.usuarioAcessoServico = usuarioAcessoServico;
            this.notificacoesServico = notificacoesServico;
            this.usuariosNotificacoesRepositorio = usuariosNotificacoesRepositorio;
        }

        public UsuarioNotificacao Instanciar(int idUsuario, int IdNotificacao)
        {
            var usuario = usuarioAcessoServico.Validar(idUsuario);
            var notificacao = notificacoesServico.Validar(IdNotificacao);


            return new UsuarioNotificacao(usuario, notificacao, false);
        }

        public UsuarioNotificacao Inserir(UsuarioNotificacao usuarioNotificacao)
        {
           usuariosNotificacoesRepositorio.Adicionar(usuarioNotificacao);

            return usuarioNotificacao;
        }

        public UsuarioNotificacao Atualizar(int codigo, bool visualizado)
        {
            UsuarioNotificacao usuarioNotificacao = Validar(codigo);

            usuarioNotificacao.SetVisualizado(visualizado);

            return usuarioNotificacao;
        }

        public UsuarioNotificacao Validar(int codigo)
        {
            return usuariosNotificacoesRepositorio.PesquisarPor(codigo);
        }
    }
}
