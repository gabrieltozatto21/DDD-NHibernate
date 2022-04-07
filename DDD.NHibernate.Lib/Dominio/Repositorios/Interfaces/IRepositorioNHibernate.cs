using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Dominio.Libs.Repositorios
{
    public interface IRepositorioNHibernate<T> where T : class
    {
        IQueryable<T> ListarTodos();
        IQueryable<T> ListarTodos(int index, int count);
        T PesquisarPor(int id);
        void Adicionar(T entity);
        void Editar(T entidade);
        void Remover(T entity);
    }
}
