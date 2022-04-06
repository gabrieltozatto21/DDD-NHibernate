using DDD.NHibernate.Aplicacao.Despesas.Servicos.Interfaces;
using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Dominio.Despesas.Repositorios;
using DDD.NHibernate.Dominio.Despesas.Servicos.Interfaces;
using DDD.NHibernate.Libs.Aplicacao.Transacoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Aplicacao.Despesas.Servicos
{
    public class DespesaAppServico : IDespesaAppServico
    {
        private readonly IDespesaServico despesaServico;
        private readonly IDespesaRepositorio despesaRepositorio;
        private readonly IUnitOfWork unitOfWork;

        public DespesaAppServico(
            IDespesaServico despesaServico, 
            IDespesaRepositorio despesaRepositorio, 
            IUnitOfWork unitOfWork
        )
        {
            this.unitOfWork = unitOfWork;
            this.despesaServico = despesaServico;
            this.despesaRepositorio = despesaRepositorio;
        }
        public IEnumerable<Despesa> Listar()
        {
            var query = despesaRepositorio.ListarTodos();

            List<Despesa> resultado = query.ToList();

            return resultado;
        }

        public Despesa Inserir(Despesa request)
        {
            try
            {
                unitOfWork.BeginTransaction();

                despesaRepositorio.Adicionar(request);

                var response = new Despesa(request.Id, request.Descricao, request.Tipo, request.NumPagamentos, request.ValorTotal);

                unitOfWork.Commit();

                return response;

            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
        public Despesa Editar(int id, Despesa request)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            try
            {
                unitOfWork.BeginTransaction();
                Despesa despesa = despesaServico.Validar(id);

                despesaRepositorio.Remover(despesa);

                unitOfWork.Commit();

            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public Despesa Recuperar(int id)
        {
            Despesa despesa = despesaServico.Validar(id);

            return despesa;
     
        }
    }
}
