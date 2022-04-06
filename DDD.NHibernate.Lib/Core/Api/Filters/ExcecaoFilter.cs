using DDD.NHibernate.Libs.Dominio.Excecoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DDD.NHibernate.Libs.Core.Api.Filters
{
    public class ExcecaoFilter : ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public ExcecaoFilter(ILogger<ExcecaoFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception.InnerException ?? context.Exception;

            if (exception is RegraDeNegocioExcecao)
            {
                context.HttpContext.Response.StatusCode = 400;
                context.Result = new JsonResult(new
                {
                    Message = exception.Message,
                    Tipo = exception.GetType().Name
                });

                return;
            }
        }

    }
}
