using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;
using FluentNHibernate.Mapping;

namespace DDD.NHibernate.Infra.UsuariosAcesso.Mapeamentos
{
    public class UsuarioAcessoMap : ClassMap<UsuarioAcesso>
    {
        public UsuarioAcessoMap()
        {
            Table("NHIBERNATEDB.USUARIOACESSO");
            Id(p => p.Id).Column("ID");
            Map(p => p.Nome).Column("NOME");
            Map(p => p.Login).Column("LOGIN");
            Map(p => p.Email).Column("EMAIL");
            Map(p => p.Senha).Column("SENHA");


        }
    }
}
