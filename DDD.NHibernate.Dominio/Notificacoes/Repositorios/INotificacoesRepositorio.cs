using DDD.NHibernate.Dominio.Libs.Repositorios;
using DDD.NHibernate.Dominio.Notificacoes.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Dominio.Notificacoes.Servicos.Interfaces
{
    public interface INotificacoesRepositorio : IRepositorioNHibernate<Notificacao>
    {
    }
}
