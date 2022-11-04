using FluentNHibernate.Mapping;
using DDD.NHibernate.Dominio.Notificacoes.Entidades;

namespace DDD.NHibernate.Infra.Notificacoes.Mapeamentos
{
    public class NotificacoesMap : ClassMap<Notificacao>
    {
        public NotificacoesMap()
        {
            Table("Notificacao");
        }
    }
}
