using DDD.NHibernate.Dominio.Despesas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Aplicacao.Despesas.Servicos.Interfaces
{
    public interface IDespesaAppServico
    {
        IEnumerable<Despesa> Listar();
        Despesa Recuperar(int id);
        Despesa Inserir(Despesa request);
        Despesa Editar(int id, Despesa request);
        void Excluir(int id);
    }
}
