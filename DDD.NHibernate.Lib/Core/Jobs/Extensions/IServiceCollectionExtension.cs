using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DDD.NHibernate.Libs.Core.Jobs.Extensions
{
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// Adiciona como transient todas as classes que implementam IJob dentro do assembly
        /// </summary>
        public static void AddJobs(this IServiceCollection services)
        {
            GetTypesByInterface<IJob>()
               .ForEach(type => services.AddTransient(type));
        }
        private static List<Type> GetTypesByInterface<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                                    .SelectMany(s => s.GetTypes())
                                    .Where(p => typeof(T).GetTypeInfo().IsAssignableFrom(p))
                                    .Where(X => X.Name != typeof(T).GetTypeInfo().Name)
                                    .ToList();
        }
    }
}
