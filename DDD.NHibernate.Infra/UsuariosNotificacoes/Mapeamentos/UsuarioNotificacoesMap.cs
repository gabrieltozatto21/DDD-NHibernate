using DDD.NHibernate.Dominio.Notificacoes.Entidades;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace DDD.NHibernate.Infra.UsuariosNotificacoes.Mapeamentos
{
    public class UsuarioNotificacoesMap : ClassMap<UsuarioNotificacao>
    {
        public UsuarioNotificacoesMap()
        {
            Table("UsuarioNotificacao");

            Id(n => n.Id).Column("ID").GeneratedBy.Identity();

            References(x => x.Usuario).Column("USUARIO");
            References(x => x.Notificacao).Column("NOTIFICACAO");
            Map(x => x.Notificado).Column("NOTIFICADO").CustomType<bool>();
            Map(x => x.Visualizado).Column("VISUALIZADO").CustomType<BooleanType>();

        }
    }
}
