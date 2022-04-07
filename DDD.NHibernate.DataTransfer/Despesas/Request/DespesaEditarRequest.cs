
using DDD.NHibernate.Dominio.Despesas.Entidades.Enumeradores;

namespace DDD.NHibernate.DataTransfer.Despesas.Request
{
    public class DespesaEditarRequest
    {
        public string Descricao { get; set; }
        public TipoDespesaEnum Tipo { get; set; }
        public int NumPagamentos { get; set; }
        public double ValorTotal { get; set; }
    }
}
