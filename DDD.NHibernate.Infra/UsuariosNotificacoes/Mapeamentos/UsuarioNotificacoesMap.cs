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

            References(x => x.Usuario, "USUARIO");
            References(x => x.Notificacao, "NOTIFICACAO");
            Map(x => x.Visualizado).Column("VISUALIZADO").CustomType<BooleanType>();


        }
    }
}
