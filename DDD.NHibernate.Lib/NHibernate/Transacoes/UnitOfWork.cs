using DDD.NHibernate.Libs.Aplicacao.Transacoes.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Libs.NHibernate.Transacoes
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ISession session;
        private ITransaction transaction;

        public UnitOfWork(ISession session)
        {
            this.session = session;
        }

        public void BeginTransaction()
        {
            this.transaction = session.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction != null && transaction.IsActive)
            {
                transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (transaction != null && transaction.IsActive)
            {
                transaction.Rollback();
            }
        }


        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }

            if (session.IsOpen)
            {
                session.Close();
                session = null;
            }
        }

        public void Limpar()
        {
            if (session != null)
                session.Clear();
        }

        public void Flush()
        {
            if (session != null)
                session.Flush();
        }
    }
}
