using System;
using DDD.NHibernate.Dominio.Despesas.Entidades.Enumeradores;
using DDD.NHibernate.Libs.Dominio.Excecoes;

namespace DDD.NHibernate.Dominio.Despesas.Entidades
{
    public class Despesa
    {
        public virtual int Id { get; protected set; }
        public virtual string Descricao { get; set; }
        public virtual TipoDespesaEnum Tipo { get; set; }
        public virtual int NumPagamentos { get; set; }
        public virtual double ValorTotal { get; set; }
        public virtual DateTime DataVencimento { get; set; }

        public Despesa(){ }
        public Despesa(string descricao, TipoDespesaEnum tipo, int numPagamentos, double valorTotal, DateTime dataVencimento)
        {
            SetDescricao(descricao);
            SetTipo(tipo);
            SetNumPagamentos(numPagamentos);
            SetValorTotal(valorTotal);
            SetDataVencimento(dataVencimento);
        }

        public virtual void SetDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
            {
                throw new AtributoObrigatorioExcecao(nameof(this.Descricao));
            }
            this.Descricao = descricao;
        }

        public virtual void SetTipo(TipoDespesaEnum tipo)
        {
            this.Tipo = tipo;
        }
        public virtual void SetDataVencimento(DateTime dataVencimento)
        {
            this.DataVencimento = dataVencimento;
        }

        public virtual void SetNumPagamentos(int numPagamentos)
        {
            this.NumPagamentos = numPagamentos;
        }

        public virtual void SetValorTotal(double valorTotal)
        {
            this.ValorTotal = valorTotal;
        }
    }
}
