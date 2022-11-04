using DDD.NHibernate.Dominio.Despesas.Entidades;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Infra.Despesas.Mapeamentos
{
    public class DespesaMap : ClassMap<Despesa>
    {
        public DespesaMap()
        {
            Table("DESPESA");
            Id(d => d.Id).Column("Id");
            Map(d => d.Descricao).Column("Descricao");
            Map(d => d.Tipo).Column("Tipo");
            Map(d => d.NumPagamentos).Column("NumPagamentos");
            Map(d => d.DataVencimento).Column("DataVencimento");
            Map(d => d.ValorTotal).Column("ValorTotal").Not.Nullable();
            References(d => d.Usuario).Column("IDUSUARIO").Nullable();
        }
    }
}
