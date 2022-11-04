using DDD.NHibernate.Dominio.Notificacoes.Entidades;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using FluentNHibernate.Mapping;

namespace DDD.NHibernate.Infra.UsuariosNotificacoes.Mapeamentos
{
    public class UsuarioNotificacoesMap : ClassMap<UsuarioNotificacoes>
    {
        public UsuarioNotificacoesMap()
        {
            Table("UsuarioNotificacao");

        }
    }
}
