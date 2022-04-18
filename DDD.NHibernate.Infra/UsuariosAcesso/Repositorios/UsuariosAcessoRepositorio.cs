using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;
using DDD.NHibernate.Lib.NHibernate.Repositorios;
using Microsoft.Extensions.Configuration;
using NHibernate;

namespace DDD.NHibernate.Infra.UsuariosAcesso.Repositorios
{
    public class UsuariosAcessoRepositorio : RepositorioNHibernate<UsuarioAcesso>
    {
        private IConfiguration configuracao;
        public UsuariosAcessoRepositorio(ISession session, IConfiguration configuracao) : base(session)
        {
            this.configuracao = configuracao;
        }

        public string GerarTokenJwt(SessaoAcesso sessao)
        {
            string secret = configuracao.GetSection("Jwt:Secret").Value;

            return "";
        }
    }
}
