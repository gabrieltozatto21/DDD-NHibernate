using DDD.NHibernate.Dominio.Despesas.Entidades;
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
        Despesa Instanciar(int idDespesa);
        Despesa Atualizar(int id, Despesa despesaAtualizada);
    }
}
