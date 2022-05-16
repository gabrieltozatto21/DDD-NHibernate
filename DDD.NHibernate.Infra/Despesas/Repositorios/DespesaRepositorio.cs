using Dapper;
using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Dominio.Despesas.Repositorios;
using DDD.NHibernate.Lib.NHibernate.Repositorios;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace DDD.NHibernate.Infra.Despesas.Repositorios
{
    public class DespesaRepositorio : RepositorioNHibernate<Despesa>, IDespesaRepositorio
    {
        public DespesaRepositorio(ISession session) : base(session) { }

        public Despesa retornaDespesaPorId(int id)
        {
            var sql = @"select d.descricao, d.id, d.tipo, d.numpagamentos, d.valortotal 
                        from despesa d 
                        where id = @PID";

            var parametros = new DynamicParameters();
            parametros.Add("PID", id);

            var comando = this.session.Connection.CreateCommand();
            this.session.Flush();
            this.session.Transaction.Enlist(comando);

            var response = session.Connection.Query<Despesa>(sql, parametros, comando.Transaction).FirstOrDefault();
            return response;
        }
    }
}
