using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Libs.Core.Jobs.Factorys
{
    public class ScheduledJobFactory : IJobFactory, IDisposable
    {
        protected readonly IServiceProvider serviceProvider;
        protected readonly ConcurrentDictionary<IJob, IServiceScope> escopos = new ConcurrentDictionary<IJob, IServiceScope>();

        public ScheduledJobFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var escopo = serviceProvider.CreateScope();
            var job = escopo.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;

            this.escopos.TryAdd(job, escopo);

            return job;
        }

        public void ReturnJob(IJob job)
        {
            try
            {
                (job as IDisposable)?.Dispose();

                if (this.escopos.TryRemove(job, out IServiceScope escopo))
                    escopo.Dispose();
            }
            catch (Exception) { }
        }

        public void Dispose()
        {
            try
            {
                foreach (var escopo in this.escopos.Values)
                {
                    escopo?.Dispose();
                }
            }
            catch (Exception) { }
        }
    }
}
