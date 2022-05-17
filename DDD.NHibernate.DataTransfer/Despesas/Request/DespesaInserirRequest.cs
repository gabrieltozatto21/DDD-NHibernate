using DDD.NHibernate.Dominio.Despesas.Entidades.Enumeradores;
using System;

namespace DDD.NHibernate.DataTransfer.Despesas.Request
{
    public class DespesaInserirRequest
    {
        public string Descricao { get; set; }
        public TipoDespesaEnum Tipo { get; set; }
        public int NumPagamentos { get; set; }
        public double ValorTotal { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
