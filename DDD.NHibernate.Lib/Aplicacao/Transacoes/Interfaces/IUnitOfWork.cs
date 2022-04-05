using System;

namespace DDD.NHibernate.Libs.Aplicacao.Transacoes.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Iniciar uma transação no banco de dados
        void BeginTransaction();

        /// <summary>
        /// Desfazer as alterações realizadas em uma transação
        /// </summary>
        void Rollback();

        /// <summary>
        /// Aplicar as alterações realizadas em uma transação
        /// </summary>
        void Commit();

        /// <summary>
        /// Limpa a sessão, cancelando operações de inclusões, atualizações e exclusões pendentes
        /// </summary>
        void Limpar();

        /// <summary>
        /// Força o flush da sessão
        /// </summary>
        void Flush();
    }
}
