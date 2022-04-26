using DDD.NHibernate.Dominio.Libs.Repositorios;
using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;

namespace DDD.NHibernate.Dominio.UsuariosAcesso.Repositorios
{
    public interface IUsuarioAcessoRepositorio : IRepositorioNHibernate<UsuarioAcesso>
    {
        string CriptografarSenhaAcesso(string login, string senha);
        string GerarTokenJwt(SessaoAcesso sessao);
    }
}
