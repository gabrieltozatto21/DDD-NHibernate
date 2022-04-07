using AutoMapper;
using DDD.NHibernate.Aplicacao.Despesas.Servicos.Interfaces;
using DDD.NHibernate.DataTransfer.Despesas.Request;
using DDD.NHibernate.DataTransfer.Despesas.Response;
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
        private readonly IMapper mapper;
        private readonly IDespesaServico despesaServico;
        private readonly IDespesaRepositorio despesaRepositorio;
        private readonly IUnitOfWork unitOfWork;

        public DespesaAppServico(
            IDespesaServico despesaServico, 
            IDespesaRepositorio despesaRepositorio, 
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.despesaServico = despesaServico;
            this.despesaRepositorio = despesaRepositorio;
        }
        public IEnumerable<DespesaResponse> Listar()
        {
            var query = despesaRepositorio.ListarTodos();

            List<Despesa> resultado = query.ToList();

            var response = mapper.Map<List<DespesaResponse>>(resultado);

            return response;
        }

        public DespesaResponse Inserir(DespesaInserirRequest request)
        {
            try
            {
                unitOfWork.BeginTransaction();

                Despesa despesa = despesaServico.Instanciar(request.Descricao, request.Tipo, request.NumPagamentos, request.ValorTotal);

                despesaRepositorio.Adicionar(despesa);

                var response = mapper.Map<DespesaResponse>(despesa);

                unitOfWork.Commit();

                return response;

            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
        public DespesaResponse Editar(int id, Despesa request)
        {
            try
            {
                unitOfWork.BeginTransaction();

                Despesa despesa = despesaServico.Atualizar(id, request);

                despesaRepositorio.Editar(despesa);

                DespesaResponse resultado = mapper.Map<DespesaResponse>(despesa);

                unitOfWork.Commit();

                return resultado;

            }
            catch
            {
                unitOfWork.Rollback();

                throw;
            }
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

        public DespesaResponse Recuperar(int id)
        {
            Despesa despesa = despesaServico.Validar(id);

            var resultado = mapper.Map<DespesaResponse>(despesa);

            return resultado;
     
        }
    }
}
