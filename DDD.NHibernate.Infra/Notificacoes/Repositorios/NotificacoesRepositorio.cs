using DDD.NHibernate.Dominio.Notificacoes.Entidades;
using DDD.NHibernate.Dominio.Notificacoes.Servicos.Interfaces;
using DDD.NHibernate.Lib.NHibernate.Repositorios;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Infra.Notificacoes.Repositorios
{
    public class NotificacoesRepositorio : RepositorioNHibernate<Notificacao>, INotificacoesRepositorio
    {
        public NotificacoesRepositorio(ISession session) : base(session)
        {
        }
    }
}
