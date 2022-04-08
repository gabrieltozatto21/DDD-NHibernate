using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Dominio.Despesas.Entidades.Enumeradores;
using DDD.NHibernate.Dominio.Despesas.Repositorios;
using DDD.NHibernate.Dominio.Despesas.Servicos.Interfaces;
using DDD.NHibernate.Libs.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Dominio.Despesas.Servicos
{
    public class DespesaServico : IDespesaServico
    {
        private readonly IDespesaRepositorio despesaRepositorio;

        public DespesaServico(IDespesaRepositorio despesaRepositorio)
        {
            this.despesaRepositorio = despesaRepositorio;
        }

        public Despesa Atualizar(int id, string descricao, TipoDespesaEnum tipo, int numPagamentos, double valorTotal)
        {
            Despesa despesa = Validar(id);

            despesa.SetDescricao(descricao);
            despesa.SetTipo(tipo);
            despesa.SetNumPagamentos(numPagamentos);
            despesa.SetValorTotal(valorTotal);

            return despesa;
        }

        public Despesa Instanciar(string descricao, TipoDespesaEnum tipo, int numPagamentos, double valorTotal)
        {
            Despesa despesa = new Despesa(descricao, tipo, numPagamentos, valorTotal);

            return despesa;
        }

        public Despesa Validar(int idDespesa)
        {
            Despesa despesa = despesaRepositorio.PesquisarPor(idDespesa);

            if(despesa == null)
            {
                throw new RegraDeNegocioExcecao("Despesa não encontrada!");
            }

            return despesa;

        }
    }
}
