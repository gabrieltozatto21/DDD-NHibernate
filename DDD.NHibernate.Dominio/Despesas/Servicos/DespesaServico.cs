using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Dominio.Despesas.Repositorios;
using DDD.NHibernate.Dominio.Despesas.Servicos.Interfaces;
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
        public Despesa Atualizar(int id, Despesa despesaAtualizada)
        {
            throw new NotImplementedException();
        }

        public Despesa Instanciar(int idDespesa)
        {
            throw new NotImplementedException();
        }

        public Despesa Validar(int idDespesa)
        {
            Despesa despesa = despesaRepositorio.PesquisarPor(idDespesa);

            if(despesa == null)
            {
                throw new Exception("Despesa não encontrada");
            }

            return despesa;

        }
    }
}
