using FluentNHibernate.Mapping;
using DDD.NHibernate.Dominio.Notificacoes.Entidades;
using NHibernate.Type;

namespace DDD.NHibernate.Infra.Notificacoes.Mapeamentos
{
    public class NotificacoesMap : ClassMap<Notificacao>
    {
        public NotificacoesMap()
        {
            Table("Notificacao");

            Id(n => n.Id).Column("ID").GeneratedBy.Identity();

            Map(n => n.DataCriacao).Column("DATANOTIFICACAO");
            Map(n => n.DataExibicao).Column("DATAEXIBICAO");
            Map(n => n.Descricao).Column("DESCNOTIFICACAO");
            Map(n => n.Link).Column("DESCLINK");
            Map(n => n.Tipo).Column("TIPO");
            Map(x => x.Ativo).Column("ATIVO").CustomType<BooleanType>();
            //HasMany(n => n.UsuarioNotificacoes).Table("USUARIONOTIFICACAO").Cascade.All().Inverse();
        }
    }
}
