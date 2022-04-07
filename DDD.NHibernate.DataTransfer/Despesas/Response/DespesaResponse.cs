using DDD.NHibernate.Dominio.Despesas.Entidades.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.DataTransfer.Despesas.Response
{
    public class DespesaResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TipoDespesaEnum Tipo { get; set; }
        public int NumPagamentos { get; set; }
        public double ValorTotal { get; set; }
    }
}
