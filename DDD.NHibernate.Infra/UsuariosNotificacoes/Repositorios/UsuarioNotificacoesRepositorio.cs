using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Repositorios;
using DDD.NHibernate.Lib.NHibernate.Repositorios;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Infra.UsuariosNotificacoes.Repositorios
{
    public class UsuarioNotificacoesRepositorio : RepositorioNHibernate<UsuarioNotificacao>, IUsuariosNotificacoesRepositorio
    {
        public UsuarioNotificacoesRepositorio(ISession session) : base(session)
        {
        }
    }
}
