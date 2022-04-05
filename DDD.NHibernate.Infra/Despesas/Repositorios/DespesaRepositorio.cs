using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Dominio.Despesas.Repositorios;
using DDD.NHibernate.Lib.NHibernate.Repositorios;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Infra.Despesas.Repositorios
{
    public class DespesaRepositorio : RepositorioNHibernate<Despesa>, IDespesaRepositorio
    {
        public DespesaRepositorio(ISession session) : base(session) { }


    }
}
