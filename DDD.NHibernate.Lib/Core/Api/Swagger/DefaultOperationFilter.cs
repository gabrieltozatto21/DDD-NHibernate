using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace DDD.NHibernate.Libs.Core.Api.Swagger
{
    public class DefaultOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>();

            operation.Responses.Add("400", new OpenApiResponse { Description = "Requisição inválida. Maiores detalhes na mensagem de retorno" });
            operation.Responses.Add("401", new OpenApiResponse { Description = "Usuário não autenticado" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Usuário não tem permissão para executar esta operação" });
            operation.Responses.Add("404", new OpenApiResponse { Description = "Recurso não encontrado" });
            operation.Responses.Add("500", new OpenApiResponse { Description = "Erro interno no servidor" });

            if (authAttributes.Any())
            {
                if (operation.Parameters == null)
                {
                    operation.Parameters = new List<OpenApiParameter>();
                }

                var myObjectSchema = context.SchemaGenerator.GenerateSchema(typeof(string), context.SchemaRepository);
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Token_Autorizacao",
                    In = ParameterLocation.Header,
                    Schema = myObjectSchema,
                    Required = true
                });
            }
        }
    }
}
