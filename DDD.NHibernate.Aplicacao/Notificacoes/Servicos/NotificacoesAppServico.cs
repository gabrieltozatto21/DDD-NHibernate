using DDD.NHibernate.Aplicacao.Notificacoes.Servicos.Interfaces;
using DDD.NHibernate.Dominio.Notificacoes.Entidades;
using DDD.NHibernate.Dominio.Notificacoes.Servicos.Interfaces;
using DDD.NHibernate.Libs.Aplicacao.Transacoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Aplicacao.Notificacoes.Servicos
{
    public class NotificacoesAppServico : INotificacoesAppServico
    {
        private readonly INotificacoesServico notificacaoServico;
        private readonly INotificacoesRepositorio notificacoesRepositorio;
        private readonly IUnitOfWork unitOfWork;

        public NotificacoesAppServico(
            INotificacoesServico notificacaoServico,
            INotificacoesRepositorio notificacoesRepositorio,
            IUnitOfWork unitOfWork)
        {
            this.notificacaoServico = notificacaoServico;
            this.notificacoesRepositorio = notificacoesRepositorio;
            this.unitOfWork = unitOfWork;
        }

        public Notificacao InserirNotificacao(Notificacao request)
        {
            try
            {
                unitOfWork.BeginTransaction();

                Notificacao notificacao = notificacaoServico.Instanciar(request.DataCriacao, request.DataExibicao, request.Descricao,
                    request.Link, request.Ativo, request.Tipo);

                notificacaoServico.Inserir(notificacao);

                unitOfWork.Commit();

                return notificacao;
            }
            catch
            {
                unitOfWork.Rollback();

                throw;
            }
        }

    }
}
