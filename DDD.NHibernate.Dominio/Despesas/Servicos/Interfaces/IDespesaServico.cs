using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Dominio.Despesas.Entidades.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Dominio.Despesas.Servicos.Interfaces
{
    public interface IDespesaServico
    {
        Despesa Validar(int idDespesa);
        Despesa Instanciar(string descricao, TipoDespesaEnum tipo, int numPagamentos, double valorTotal, DateTime dataVencimento);
        Despesa Atualizar(int id, string descricao, TipoDespesaEnum tipo, int numPagamentos, double valorTotal);
        IList<Despesa> AplicaJuros();
    }
}
