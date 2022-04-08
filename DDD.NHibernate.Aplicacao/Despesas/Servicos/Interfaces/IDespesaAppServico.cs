using DDD.NHibernate.DataTransfer.Despesas.Request;
using DDD.NHibernate.DataTransfer.Despesas.Response;
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
        IEnumerable<DespesaResponse> Listar();
        DespesaResponse Recuperar(int id);
        DespesaResponse Inserir(DespesaInserirRequest request);
        DespesaResponse Editar(int id, DespesaEditarRequest request);
        void Excluir(int id);
    }
}
