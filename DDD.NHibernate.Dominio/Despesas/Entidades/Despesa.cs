using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Dominio.Despesas.Entidades
{
    public class Despesa
    {
        public virtual int Id { get; protected set; }
        public virtual string Descricao { get; set; }
        public virtual string Tipo { get; set; }
        public virtual int NumPagamentos { get; set; }
        public virtual double ValorTotal { get; set; }

        public Despesa(){ }
        public Despesa(int id, string descricao, string tipo, int numPagamentos, double valorTotal)
        {
            Id = id;
            Descricao = descricao;
            Tipo = tipo;
            NumPagamentos = numPagamentos;
            ValorTotal = valorTotal;
        }
    }
}
