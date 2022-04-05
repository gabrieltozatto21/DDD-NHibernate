using DDD.NHibernate.Dominio.Libs.Repositorios;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Lib.NHibernate.Repositorios
{
    public class RepositorioNHibernate<T> : IRepositorioNHibernate<T> where T : class
    {
        protected readonly ISession session;

        public RepositorioNHibernate(ISession session)
        {
            this.session = session;
        }

        public IQueryable<T> ListarTodos()
        {
            return session.Query<T>();
        }
        public void Adicionar(T entity)
        {
            session.Save(entity);
        }
        public IQueryable<T> ListarTodos(int index, int count)
        {
            throw new NotImplementedException();
        }
        public T PesquisarPor(int id)
        {
            T teste = session.Get<T>(id);
            return teste;
        }
        public void Remover(T entity)
        {
            session.Delete(entity);
        }
    }
}
