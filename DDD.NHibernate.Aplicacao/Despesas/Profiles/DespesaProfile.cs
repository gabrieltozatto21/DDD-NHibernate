using AutoMapper;
using DDD.NHibernate.DataTransfer.Despesas.Response;
using DDD.NHibernate.Dominio.Despesas.Entidades;
namespace DDD.NHibernate.Aplicacao.Despesas.Profiles
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            CreateMap<Despesa, DespesaResponse>();
        }

    }
}
