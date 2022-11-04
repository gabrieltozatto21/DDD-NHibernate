using DDD.NHibernate.Dominio.Notificacoes.Entidades;
using DDD.NHibernate.Dominio.Notificacoes.Servicos.Interfaces;
using DDD.NHibernate.Dominio.UsuariosAcesso.Repositorios;
using DDD.NHibernate.Dominio.UsuariosAcesso.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Dominio.Notificacoes.Servicos
{
    public class NotificacoesServico : INotificacoesServico
    {
        private readonly IUsuarioAcessoServico usuarioRepositorio;
        private readonly INotificacoesRepositorio notificacoesRepositorio;

        public NotificacoesServico(
            IUsuarioAcessoServico usuarioRepositorio,
            INotificacoesRepositorio notificacoesRepositorio
            )
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.notificacoesRepositorio = notificacoesRepositorio;
        }

        public Notificacao Instanciar(DateTime dataCriacao, DateTime? dataExibicao, string descricao, string link, bool ativo, int tipo)
        {
            var notificacao = new Notificacao(dataCriacao, dataExibicao, descricao, link, ativo, tipo);

            return notificacao;
        }

        public Notificacao Inserir(Notificacao notificacao)
        {
            notificacoesRepositorio.Adicionar(notificacao);

            return notificacao;
        }

        public Notificacao Validar(int idNotificacao)
        {
            return notificacoesRepositorio.PesquisarPor(idNotificacao);
        }
    }
}
