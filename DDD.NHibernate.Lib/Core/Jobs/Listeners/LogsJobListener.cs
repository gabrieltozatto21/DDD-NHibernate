using Microsoft.Extensions.Logging;
using Quartz;
using Serilog.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.NHibernate.Libs.Core.Jobs.Listeners
{
    public class LogsJobListener : IJobListener
    {
        private readonly ILogger logger;

        public LogsJobListener(ILogger<LogsJobListener> logger)
        {
            this.logger = logger;
        }

        public string Name => "LogsJobsListener";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            LogContext.PushProperty("TransactionId", context.FireInstanceId);

            try
            {
                DateTime? scheduledTime = context.ScheduledFireTimeUtc.HasValue ? context.ScheduledFireTimeUtc.Value.DateTime.ToLocalTime() : DateTime.MinValue;
                DateTime? fireTime = context.FireTimeUtc.LocalDateTime;
                DateTime? previousFireTime = context.PreviousFireTimeUtc.HasValue ? context.PreviousFireTimeUtc.Value.DateTime.ToLocalTime() : DateTime.MinValue;
                this.logger.LogInformation("<{EventoId}> {Job} - Agendado para {ScheduledTime} /  Iniciado em {FireTime} / Última Execução em {PreviousFireTime}",
                    "JobToBeExecuted",
                    context.JobDetail.JobType.Name,
                    scheduledTime,
                    fireTime,
                    previousFireTime);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "<{EventoId}> - Ocorreu um erro interno.", "JobToBeExecuted");
            }

            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            LogContext.PushProperty("TransactionId", context.FireInstanceId);

            try
            {
                this.logger.LogInformation("<{EventoId}> {Job} -  Executado em {Elapsed:0.0000} segundos",
                       "JobWasExecuted",
                       context.JobDetail.JobType.Name,
                       context.JobRunTime.TotalSeconds);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "<{EventoId}> - Ocorreu um erro interno.", "JobWasExecuted");
            }

            return Task.CompletedTask;
        }
    }
}
