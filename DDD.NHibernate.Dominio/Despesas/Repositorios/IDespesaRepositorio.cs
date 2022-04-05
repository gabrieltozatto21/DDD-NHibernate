using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Dominio.Libs.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Dominio.Despesas.Repositorios
{
    public interface IDespesaRepositorio : IRepositorioNHibernate<Despesa>
    {

    }
}
