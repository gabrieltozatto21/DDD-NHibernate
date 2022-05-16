using Quartz;
using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Aplicacao.Despesas.Servicos.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DDD.Nhibernate.Jobs.Jobs.DespesasJobs
{
    public class DespesaValorJob : IJob
    {
        private readonly IDespesaAppServico despesaAppServico;

        public DespesaValorJob(IDespesaAppServico despesaAppServico)
        {
            this.despesaAppServico = despesaAppServico;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            Despesa despesa = (Despesa)dataMap.Get("despesa");

            string mensagem = dataMap.GetString("mensagem");

            despesaAppServico.AlterarValor(1);

            await Task.CompletedTask;
        }
    }
}
